using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;
using AspNetCoreAssessment2.Foundation.Identity;
using AspNetCoreAssessment2.Foundation.Interfaces;
using AspNetCoreAssessment2.Foundation.Services;
using AspNetCoreAssessment2.Foundation.Stores;
using AspNetCoreAssessment2.Web.HealthChecks;
using AspNetCoreAssessment2.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
namespace AspNetCoreAssessment2.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddUserStore<UserStore>()
                .AddSignInManager()
                .AddClaimsPrincipalFactory<ReadRulesClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(o =>
            {
                o.ValidationInterval = TimeSpan.FromSeconds(30);
            });

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme, o =>
                {
                    o.LoginPath = "/Account/Login";
                    o.Events.OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync;
                })
                .AddCookie(IdentityConstants.ExternalScheme)
                .AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
                {
                    o.Events.OnValidatePrincipal = SecurityStampValidator.ValidateAsync<ITwoFactorSecurityStampValidator>;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdditionalUserClaims.HasReadRules, policy =>
                    policy.RequireClaim(AdditionalUserClaims.HasReadRules, AdditionalUserClaims.AllowedHasReadRulesValues));
            });

            services.AddScoped<IAccountService, AccountService>();

            services.AddHealthChecks()
                .AddCheck<ResponseTimeHealthCheck>("Response time speed", null, new[] { "service" });

            services.AddSingleton<ResponseTimeHealthCheck>();

            services.AddControllersWithViews();
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=60000");
                }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "StaticFiles")),
                RequestPath = "/StaticFiles",
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "StaticFiles")),
                RequestPath = "/StaticFiles"
            });

            app.UseLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = WriteResponse
                });
            });
        }


        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("status", result.Status.ToString());
                    writer.WriteStartObject("results");
                    foreach (var (key, value) in result.Entries)
                    {
                        writer.WriteStartObject(key);
                        writer.WriteString("status", value.Status.ToString());
                        writer.WriteString("description", value.Description);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                return context.Response.WriteAsync(json);
            }
        }
    }
}
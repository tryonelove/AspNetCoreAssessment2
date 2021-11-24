using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreAssessment2.Web.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        private readonly Random _rng;


        public ResponseTimeHealthCheck()
        {
            _rng = new Random();
        }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            const string response = "{0} | Response time: {1}";

            var responseTime = _rng.Next(1, 300);

            if (responseTime < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy(String.Format(response, $"We're working as expected.", responseTime)));
            }

            if (responseTime < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded(String.Format(response, $"We're slower than expected.", responseTime)));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy(String.Format(response, $"Something doesn't work at all.", responseTime)));
        }
    }
}
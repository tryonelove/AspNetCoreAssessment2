using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;
using AspNetCoreAssessment2.Foundation.Interfaces;
using AspNetCoreAssessment2.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAssessment2.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                Email = registerViewModel.Email,
                FullName = registerViewModel.FullName,
                UserName = registerViewModel.Email
            };

            await _accountService.RegisterAsync(user, registerViewModel.Password);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _accountService.LoginAsync(model.Login, model.Password);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

           return RedirectToAction("Index", "Home");
        }
    }
}
using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;
using AspNetCoreAssessment2.Foundation.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAssessment2.Foundation.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<bool> RegisterAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }

            return result.Succeeded;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
             var result = await _signInManager.PasswordSignInAsync(email, password, true, false);

             return result.Succeeded;
        }

        public async Task<bool> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return true;
        }
    }
}
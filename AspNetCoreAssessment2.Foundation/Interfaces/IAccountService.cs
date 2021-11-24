using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;

namespace AspNetCoreAssessment2.Foundation.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(User user, string password);

        Task<bool> LoginAsync(string email, string password);

        Task<bool> LogoutAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAssessment2.Foundation.Stores
{
    public class UserStore : IUserPasswordStore<User>, IUserEmailStore<User>
    {
        private static readonly IList<User> Users = new List<User>();


        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Users.Add(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userToUpdate = Users.FirstOrDefault(u => u.Id == user.Id);
            Users.Remove(userToUpdate);
            Users.Add(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Users.Remove(user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedName is null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (passwordHash is null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId is null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = Users.SingleOrDefault(u => u.Id == userId);

            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedUserName is null)
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }

            var user = Users.SingleOrDefault(u => u.NormalizedUserName == normalizedUserName);

            return Task.FromResult(user);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PasswordHash != null);
        }

        public void Dispose()
        {

        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            user.Email = email;

            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.EmailConfirmed = confirmed;

            return Task.CompletedTask;
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var user = Users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail);

            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedEmail is null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            user.NormalizedEmail = normalizedEmail;

            return Task.CompletedTask;
        }
    }
}
using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions
{
    public interface IIdentityService
    {
        Task<bool> IsAuthenticatedAsync();
        Task<bool> LoginAsync(string email, string password);
        Task<bool> LoginByEmailAsync(string email);
        Task<bool> LogOutAsync();
        Task<bool> RegisterByExternalAppAsync(UserProfile userProfile);
        Task<bool> RegisterAsync(UserProfile userProfile);
        Task ActivateProfileAsync(UserProfile userProfile);
        Task<UserProfile> GetUserLoguedAsync();
        Task SetUserLoguedAsync(UserProfile value);
    }
    public interface ILoggerService
    {
        Task LogErrorAsync(Exception exception);
    }
}
using System.Threading.Tasks;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;

namespace WhereAreYouMobile.Abstractions
{
    public interface ILoginService
    {
        Task<LoginResponseEnum> LoguinAsync(string email, string password);
        Task<bool> LoguinAsync(string email, UserProfile userProfile);
        Task<bool> LoguinAsync(UserProfile userProfile);
        Task<SignupResponseEnum> SignupAsync(string username, string password, UserProfile userProfile);
        Task SignupDummysync();
    }
}
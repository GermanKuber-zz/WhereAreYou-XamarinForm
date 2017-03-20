using System.Threading.Tasks;
using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface IAuthenticateExternalProxy
    {
        Task<UserLogin> Login(string username, string password);
    }
}
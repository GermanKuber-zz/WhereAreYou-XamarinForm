using System.Threading.Tasks;
using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface IExternaLoginService
    {
        Task<bool> Login(ExternalLoginTypeEnum externalLoginType);
    }
}
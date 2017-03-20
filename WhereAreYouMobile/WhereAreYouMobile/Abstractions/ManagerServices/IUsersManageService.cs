using System.Threading.Tasks;
using WhereAreYouMobile.Services.ManagerServices;

namespace WhereAreYouMobile.Abstractions.ManagerServices
{
    public interface IUsersManageService
    {
        Task<StatusUsers> CheckStatusUsers(string idUserMain, string idFriendUser);
        Task<UserToAddWrapper> GetUserByEmailToAdd(string email);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions.ManagerServices
{
    public interface IFriendsManageService
    {
        Task<IEnumerable<UserProfile>> GetAllFriendsAsync();
        Task DeleteFriend(UserProfile friendToDelete);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Services.ManagerServices
{
    public interface IFriendsManageService
    {
        Task<IEnumerable<UserProfile>> GetAllFriendsAsync();
    }
}
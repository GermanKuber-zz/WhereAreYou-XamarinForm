using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions.Repositories
{
    public interface IFriendsRepository : IBaseRepository<Friend>
    {
        Task<IEnumerable<UserProfile>> GetAllFriendsByIdAsync(string id);
        Task<Friend> GetFriendByBothAsync(string idUserMain, string idFriendUser);
        Task DeleteFriendAsync(string idUser, string idFriend);
    }
}
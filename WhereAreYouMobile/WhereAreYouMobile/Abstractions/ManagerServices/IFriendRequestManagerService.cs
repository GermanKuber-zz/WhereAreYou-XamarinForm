using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions.ManagerServices
{
    public interface IFriendRequestManagerService 
    {
        Task<bool> AcceptInviteAsync(FriendRequest friendRequest);
        Task<bool> RejectInviteAsync(FriendRequest friendRequest);
        
        Task<IEnumerable<FriendRequest>> GetAllRequestReceiveAsync();
    }
}
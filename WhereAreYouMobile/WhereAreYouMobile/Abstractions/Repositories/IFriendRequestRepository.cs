using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions.Repositories
{
    public interface IFriendRequestRepository
    {
        Task<IEnumerable<FriendRequest>> GetSendedRequestsAsync(string idUserSendInvitation);
        Task<bool> SendInvitationAsync(UserProfile idUserSendInvitation, UserProfile idUserDestinationInvitation);
        Task<FriendRequest> GetSendedRequestAsync(string idUserMain, string idFriendUser);
        Task<FriendRequest> GetReceivedRequsetAsync(string idUserMain, string idFriendUser);
        Task<IEnumerable<FriendRequest>> GetAllReceivedRequestsAsync(string idUserReceived);
    }
}
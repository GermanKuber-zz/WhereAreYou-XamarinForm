using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.ManagerServices;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.ManagerServices;
using Xamarin.Forms;
[assembly: Dependency(typeof(FriendRequestManagerService))]
namespace WhereAreYouMobile.Services.ManagerServices
{
    public class FriendRequestManagerService : IFriendRequestManagerService
    {

        private readonly ILoggerService _loggerService;
        private readonly IIdentityService _identityService;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IFriendsRepository _friendsRepository;

        public FriendRequestManagerService()
        {
            _loggerService = DependencyService.Get<ILoggerService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _friendsRepository = DependencyService.Get<IFriendsRepository>();
        }


        public async Task<bool> AcceptInviteAsync(FriendRequest friendRequest)
        {
            try
            {
                if (friendRequest == null)
                    throw new ArgumentNullException(nameof(friendRequest));

                friendRequest.Status = FriendRequestStatusEnum.Accepted;

                await _friendRequestRepository.SaveAsync(friendRequest);

                var userLogued = await this._identityService.GetUserLoguedAsync();

                var friend = new Friend(userLogued.Id, friendRequest.IdUserSendInvitation);

                await _friendsRepository.SaveAsync(friend);
                return true;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }

        public async Task<IEnumerable<FriendRequest>> GetAllRequestReceiveAsync()
        {
            var userLoggued = await _identityService.GetUserLoguedAsync();
            var list = await _friendRequestRepository.GetAllReceivedRequestsAsync(userLoggued.Id);
            return list;
        }

        public async Task<bool> RejectInviteAsync(FriendRequest friendRequest)
        {
            try
            {
                if (friendRequest == null)
                    throw new ArgumentNullException(nameof(friendRequest));

                friendRequest.Status = FriendRequestStatusEnum.Rejected;

                await _friendRequestRepository.SaveAsync(friendRequest);

                return true;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }
    }


}
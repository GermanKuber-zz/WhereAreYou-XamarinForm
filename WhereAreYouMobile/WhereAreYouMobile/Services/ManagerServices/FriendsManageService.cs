using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.ManagerServices;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.ManagerServices;
using Xamarin.Forms;
[assembly: Dependency(typeof(FriendsManageService))]
namespace WhereAreYouMobile.Services.ManagerServices
{
    public class FriendsManageService : IFriendsManageService
    {

        private readonly ILoggerService _loggerService;
        private readonly IIdentityService _identityService;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;

        public FriendsManageService()
        {
            _loggerService = DependencyService.Get<ILoggerService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _friendsRepository = DependencyService.Get<IFriendsRepository>();
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
        }

        public async Task<IEnumerable<UserProfile>> GetAllFriendsAsync()
        {
            try
            {
                var userLogued = await this._identityService.GetUserLoguedAsync();
                var friend = await _friendsRepository.GetAllFriendsByIdAsync(userLogued.Id);
                return friend;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }
        public async Task DeleteFriend(UserProfile friendToDelete)
        {
            try
            {
                if (friendToDelete == null)
                    throw  new ArgumentNullException(nameof(friendToDelete));

                var userLogued = await this._identityService.GetUserLoguedAsync();
                 await _friendsRepository.DeleteFriendAsync(userLogued.Id,friendToDelete.Id);
                var request = await _friendRequestRepository.GetByUsersIdAsync(userLogued.Id, friendToDelete.Id);
                request.Status = FriendRequestStatusEnum.DeletedFriend;
                request.DateDeletedFriend = DateTime.Now;
                await _friendRequestRepository.SaveAsync(request);
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }

    }
}
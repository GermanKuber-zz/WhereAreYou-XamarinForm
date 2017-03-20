using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
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

        public FriendsManageService()
        {

            _loggerService = DependencyService.Get<ILoggerService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _friendsRepository = DependencyService.Get<IFriendsRepository>();
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
       

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.ManagerServices;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.ManagerServices;
using Xamarin.Forms;
[assembly: Dependency(typeof(UsersManageService))]
namespace WhereAreYouMobile.Services.ManagerServices
{
    public class UsersManageService : IUsersManageService
    {

        private readonly ILoggerService _loggerService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IIdentityService _identityService;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IFriendsRepository _friendsRepository;

        public UsersManageService()
        {
     
            _loggerService = DependencyService.Get<ILoggerService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _userProfileRepository = DependencyService.Get<IUserProfileRepository>();
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _friendsRepository = DependencyService.Get<IFriendsRepository>();
        }
        /// <summary>
        /// Verifica el estado de 2 usuarios
        /// </summary>
        /// <param name="idUserMain">Usuario principal</param>
        /// <param name="idFriendUser">Usuario contra el que quiero chequear en que estado de relacion estan</param>
        /// <returns></returns>
        public async Task<StatusUsers> CheckStatusUsers(string idUserMain, string idFriendUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idUserMain))
                    throw new ArgumentNullException(nameof(idUserMain));
                if (string.IsNullOrWhiteSpace(idFriendUser))
                    throw new ArgumentNullException(nameof(idFriendUser));

                //Verifico si somos amigos
                var friend = await this._friendsRepository.GetFriendByBothAsync(idUserMain, idFriendUser);
                if (friend != null)
                    return StatusUsers.Friends;

                //Verifico si yo le envie una invitacion
                var invitation = await _friendRequestRepository.GetSendedRequestAsync(idUserMain, idFriendUser);

                if (invitation != null)
                {
                    if (invitation.Status == FriendRequestStatusEnum.Canceled)
                        return StatusUsers.HeCanceled;
                    if (invitation.Status == FriendRequestStatusEnum.Rejected)
                        return StatusUsers.HeRejected;
                    if (invitation.Status == FriendRequestStatusEnum.Sended)
                        return StatusUsers.YouWaitingResponse;
                }
                //Verifico si el me envio una invitacion
                var heInvitation = await this._friendRequestRepository.GetReceivedRequsetAsync(idUserMain, idFriendUser);
                if (heInvitation != null)
                {
                    if (heInvitation.Status == FriendRequestStatusEnum.Canceled)
                        return StatusUsers.YouCanceled;
                    if (heInvitation.Status == FriendRequestStatusEnum.Rejected)
                        return StatusUsers.YouRejected;
                    if (heInvitation.Status == FriendRequestStatusEnum.Sended)
                        return StatusUsers.HeWaitingResponse;
                }


                return StatusUsers.NoRelationship;
               
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }

        }





        public async Task<UserToAddWrapper> GetUserByEmailToAdd(string email)
        {
            try
            {if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentNullException(nameof(email));

                email = email.TrimStart(' ').TrimEnd(' ');
                var resultUser = await _userProfileRepository.GetByEmailAsync(email);
                if (resultUser != null)
                {
                    var logguedUser = await _identityService.GetUserLoguedAsync();
                    var resultStatus = await this.CheckStatusUsers(logguedUser.Id, resultUser.Id);
                    var newWrapper = new UserToAddWrapper(resultUser,resultStatus);
                    return newWrapper;
                }
                return null;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }

        }
    }

    public class UserToAddWrapper
    {
        public UserProfile UserProfile { get;  }
        public StatusUsers StatusUsers { get;  }

        public UserToAddWrapper(UserProfile userProfile, StatusUsers statusUsers)
        {
            this.UserProfile = userProfile;
            this.StatusUsers = statusUsers;
        }
    }

    public enum StatusUsers
    {
        Friends,
        //Tu enviaste la invitacion y estas esperando una respuesta de parte de tu amigo
        YouWaitingResponse,
        HeWaitingResponse,
        YouCanceled,
        HeCanceled,
        YouRejected,
        HeRejected,
        NoRelationship
    }
}
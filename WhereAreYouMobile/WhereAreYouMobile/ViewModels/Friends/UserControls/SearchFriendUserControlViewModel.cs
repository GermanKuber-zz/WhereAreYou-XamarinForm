using System.Collections.Generic;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.ManagerServices;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.ManagerServices;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class SearchFriendUserControlViewModel : BaseViewModel
    {
        #region Services
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IAlertService _alertService;
        private readonly IIdentityService _identityService;
        private readonly IUsersManageService _usersManageService;

        #endregion

        #region Properties
        private bool _visibleRegister;
        public bool VisibleRegister
        {
            get { return _visibleRegister; }
            set
            {
                _visibleRegister = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleWaitingStatus;
        public bool VisibleWaitingStatus
        {
            get { return _visibleWaitingStatus; }
            set
            {
                _visibleWaitingStatus = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleCanceled;
        public bool VisibleCanceled
        {
            get { return _visibleCanceled; }
            set
            {
                _visibleCanceled = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleRejected;
        public bool VisibleRejected
        {
            get { return _visibleRejected; }
            set
            {
                _visibleRejected = value;
                OnPropertyChanged();
            }
        }
        private bool _visibleFriends;
        public bool VisibleFriends
        {
            get { return _visibleFriends; }
            set
            {
                _visibleFriends = value;
                OnPropertyChanged();
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private bool _found;
        public bool Found
        {
            get { return _found; }
            set
            {
                _found = value;
                OnPropertyChanged();
            }
        }
        private StatusUsers _statusRelationShip;
        public StatusUsers StatusRelationShip
        {
            get { return _statusRelationShip; }
            set
            {
                _statusRelationShip = value;
                OnPropertyChanged();
            }
        }

        private UserProfile _userFound;
        public UserProfile UserFound
        {
            get { return _userFound; }
            set
            {
                _userFound = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region  Commands


        public ICommand SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsLoading = true;
                    if (string.IsNullOrWhiteSpace(this.Email))
                    {
                        await _alertService.DisplayAlertAsync("Ingrese un Email valido, por favor!!");
                    }
                    else
                    {
                        var result = await _usersManageService.GetUserByEmailToAdd(this.Email);
                        if (result != null)
                        {
                            Found = true;
                            this.UserFound = result.UserProfile;
                            this.StatusRelationShip = result.StatusUsers;
                            CheckStatus();
                        }
                        else
                        {
                            this.Found = false;
                        }
                    }

                    this.IsLoading = false;
                });
            }
        }
        public ICommand AddFriendCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (UserFound != null)
                    {
                        this.IsLoading = true;
                        var loguedUser = await _identityService.GetUserLoguedAsync();
                        if (await _friendRequestRepository.SendInvitationAsync(loguedUser, this.UserFound))
                        {
                            this.StatusRelationShip = StatusUsers.YouWaitingResponse;
                            await _alertService.DisplayAlertAsync($"Se envio una solicitud de amistad a : {UserFound.Email}");
                        }
                        else
                            await _alertService.DisplayAlertAsync($"Error al enviar una solicitud de amistad a : {UserFound.Email}", "Error");

                        CheckStatus();
                        this.IsLoading = false;
                    }
                });
            }
        }



        #endregion

        #region Private Methods

        private void CheckStatus()
        {
            this.VisibleRegister = false;
            this.VisibleCanceled = false;
            this.VisibleFriends = false;
            this.VisibleWaitingStatus = false;
            this.VisibleRejected = false;

            this.VisibleRegister = this.StatusRelationShip == StatusUsers.NoRelationship;
            this.VisibleCanceled = (this.StatusRelationShip == StatusUsers.HeCanceled || this.StatusRelationShip == StatusUsers.YouCanceled);
            this.VisibleRejected = (this.StatusRelationShip == StatusUsers.HeRejected || this.StatusRelationShip == StatusUsers.YouRejected);
            this.VisibleFriends = (this.StatusRelationShip == StatusUsers.Friends);
            this.VisibleWaitingStatus = (this.StatusRelationShip == StatusUsers.HeWaitingResponse || this.StatusRelationShip == StatusUsers.YouWaitingResponse);
        }

        #endregion

        #region  Constructor

        public SearchFriendUserControlViewModel()
        {
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _alertService = DependencyService.Get<IAlertService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _usersManageService = DependencyService.Get<IUsersManageService>();
        }

        #endregion

    }
}

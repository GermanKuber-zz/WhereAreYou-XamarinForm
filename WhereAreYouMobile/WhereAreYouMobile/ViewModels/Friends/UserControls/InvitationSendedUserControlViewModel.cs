﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class InvitationSendedUserControlViewModel : BaseViewModel
    {


        #region Services

        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IIdentityService _identityService;
        private readonly IEventAgregatorService _eventAgregatorService;

        #endregion

        #region Properties

        public ObservableCollection<FriendRequest> InvitationsSended { get; set; } = new ObservableCollection<FriendRequest>();

        private FriendRequest _invitationSelected;
        public FriendRequest InvitationSelected
        {
            get { return _invitationSelected; }
            set
            {
                _invitationSelected = value;
                OnPropertyChanged();
            }
        }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region  Commands


        public ICommand AcceptInvitationCommand
        {
            get
            {
                return new Command(async () =>
                {


                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await this.LoadInvitations();

                    IsRefreshing = false;

                });
            }
        }

        public ICommand CancelInvitationCommand
        {
            get
            {
                return new Command(async () =>
                {


                });
            }
        }

        #endregion

        public InvitationSendedUserControlViewModel()
        {
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _identityService = DependencyService.Get<IIdentityService>();
            _eventAgregatorService = DependencyService.Get<IEventAgregatorService>();
            SubscribeEvents();
            LoadInvitations();
        }
        public void SubscribeEvents()
        {
            //_eventAgregatorService.Subscribe(EventAgregatorTypeEnum.UpdateSendedInvitationsFriends, async () => await LoadInvitations());
        }
        public async Task LoadInvitations()
        {
            this.IsBusy = true;
            var userLoggued = await _identityService.GetUserLoguedAsync();
            var list = await _friendRequestRepository.GetSendedRequestsAsync(userLoggued.Id);

            this.InvitationsSended.Clear();
            if (list != null)
            {
                foreach (var friendRequest in list)
                {
                    this.InvitationsSended.Add(friendRequest);
                }
            }
            this.IsBusy = false;
        }
    }
}

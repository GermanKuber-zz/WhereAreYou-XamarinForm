using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.ManagerServices;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Services.ManagerServices;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class InvitationReceivedUserControlViewModel : BaseViewModel
    {

        #region Services

        private readonly IFriendRequestManagerService _friendRequestManageService;
        private readonly IEventAgregatorService _eventAgregatorService;

        #endregion

        #region Properties

        public ObservableCollection<FriendRequest> Invitations { get; set; } = new ObservableCollection<FriendRequest>();

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
                return new Command(async (friendRequest) =>
                {
                    await this.CallWithLoadingAsync(async () =>
                    {
                        await _friendRequestManageService.AcceptInviteAsync((FriendRequest)friendRequest);
                        await this.LoadInvitations();
                        _eventAgregatorService.Raise(EventAgregatorTypeEnum.UpdateMyFriends);
                    });
                });
            }
        }

        public ICommand RejectInvitationCommand
        {
            get
            {
                return new Command(async (friendRequest) =>
                {
                    await this.CallWithLoadingAsync(async () =>
                    {
                        await _friendRequestManageService.RejectInviteAsync((FriendRequest)friendRequest);
                        await this.LoadInvitations();
                    });
                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async (s) =>
                {
                    await this.CallWithLoadingAsync(async () =>
                    {

                        await this.LoadInvitations();

                    });
                });
            }
        }



        #endregion

        public InvitationReceivedUserControlViewModel()
        {
         
            _friendRequestManageService = DependencyService.Get<IFriendRequestManagerService>();
            _eventAgregatorService = DependencyService.Get<IEventAgregatorService>();


            this.LoadInvitations();

        }

        public async Task LoadInvitations()
        {
            this.IsRefreshing = true;
            this.IsBusy = true;
            var list = await _friendRequestManageService.GetAllRequestReceiveAsync();
            this.Invitations.Clear();
            if (list != null)
            {
                foreach (var friendRequest in list)
                {
                    this.Invitations.Add(friendRequest);
                }
            }
            this.IsBusy = false;
            this.IsRefreshing = false;
        }
    }
}
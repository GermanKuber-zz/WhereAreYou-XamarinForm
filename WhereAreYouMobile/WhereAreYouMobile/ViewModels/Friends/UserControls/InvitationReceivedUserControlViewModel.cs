using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.ManagerServices;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class InvitationReceivedUserControlViewModel : BaseViewModel
    {

        #region Services

        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IIdentityService _identityService;
        private readonly IFriendRequestManagerService _friendRequestManageService;

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
                   await _friendRequestManageService.AcceptInviteAsync((FriendRequest)friendRequest);
               });
            }
        }
        public ICommand RejectInvitationCommand
        {
            get
            {
                return new Command(async (friendRequest) =>
                {
                    await _friendRequestManageService.RejectInviteAsync((FriendRequest)friendRequest);
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



        #endregion

        public InvitationReceivedUserControlViewModel()
        {
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _identityService = DependencyService.Get<IIdentityService>();
            _friendRequestManageService = DependencyService.Get<IFriendRequestManagerService>();

        }

        public async Task LoadInvitations()
        {

            var list = await _friendRequestManageService.GetAllRequestReceiveAsync();
            this.Invitations.Clear();
            if (list != null)
            {
                foreach (var friendRequest in list)
                {
                    this.Invitations.Add(friendRequest);
                }
            }
        }
    }
}
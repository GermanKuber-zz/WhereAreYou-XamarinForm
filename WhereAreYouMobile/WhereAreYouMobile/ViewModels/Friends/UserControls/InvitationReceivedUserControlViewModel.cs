using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class InvitationReceivedUserControlViewModel : BaseViewModel
    {

        #region Services

        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IIdentityService _identityService;

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
                return new Command( () =>
                {
                    var a = "";
					var adas = a;
					a = adas;

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

        public InvitationReceivedUserControlViewModel()
        {
            _friendRequestRepository = DependencyService.Get<IFriendRequestRepository>();
            _identityService = DependencyService.Get<IIdentityService>();

        }

        public async Task LoadInvitations()
        {
            var userLoggued = await _identityService.GetUserLoguedAsync();
            var list = await _friendRequestRepository.GetAllReceivedRequestsAsync(userLoggued.Id);

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
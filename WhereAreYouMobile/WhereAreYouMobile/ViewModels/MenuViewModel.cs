using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.ViewModels.User;
using WhereAreYouMobile.Views;
using WhereAreYouMobile.Views.Friends;
using WhereAreYouMobile.Views.Map;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private ObservableCollection<HomeMenuItem> _menuOptions;
        public ObservableCollection<HomeMenuItem> MenuOptions
        {
            get { return _menuOptions; }
            set
            {
                _menuOptions = value;
                OnPropertyChanged();
            }
        }

        private string _userDisplayName;
        public string UserDisplayName
        {
            get { return _userDisplayName; }
            set
            {
                _userDisplayName = value;
                OnPropertyChanged();
            }
        }
        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }
        private readonly INavigationService _navigation;
        private readonly IIdentityService _identityService;
        private HomeMenuItem _selectedOption;
        public HomeMenuItem SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
                this.SelectedOption.NavigatedCallback?.Invoke();
            }
        }

        private async Task LoadUserDataAsync()
        {
            this.UserDisplayName = (await _identityService.GetUserLoguedAsync())?.DisplayName;
            this.Image = (await _identityService.GetUserLoguedAsync())?.Image;
        }

        public MenuViewModel()
        {
            _navigation = DependencyService.Get<INavigationService>();
            _identityService = DependencyService.Get<IIdentityService>();
            this.LoadUserDataAsync();
            MenuOptions = new ObservableCollection<HomeMenuItem>
                {
                    new HomeMenuItem(()=> {
                        _navigation.NavigateAsync(new DashBoardPage());
                    }) { Title = "DashBoard",
                        Icon ="about.png" },
                         new HomeMenuItem(()=>{
                        _navigation.NavigateAsync(new MapPage());
                    }) { Title = "Mapa",
                        Icon ="about.png" },
                        new HomeMenuItem(()=>{
                       _navigation.NavigateAsync(new FriendsPage());
                    }) { Title = "Amigos",
                        Icon ="about.png" },
                        new HomeMenuItem(()=> {
                            this._identityService.LogOutAsync();
                            this._navigation.NavigateAsync(new LoginPage());
                        }) { Title = "LogOut",
                        Icon ="about.png" }
                };

        }
    }
}

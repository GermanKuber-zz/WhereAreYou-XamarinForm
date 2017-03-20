using System.Collections.ObjectModel;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.Friends.UserControls
{
    public class FriendsListUserControlViewModel : BaseViewModel
    {
        #region Services

        private readonly IUserProfileRepository _userProfileRepository;

        #endregion

        #region Properties

        public ObservableCollection<UserProfile> UsersFound { get; set; } = new ObservableCollection<UserProfile>();
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
        #endregion

        #region  Commands


        public ICommand SearchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await _userProfileRepository.GetByEmailAsync(this.Email);

                });
            }
        }

        #endregion

        public FriendsListUserControlViewModel()
        {
            _userProfileRepository = DependencyService.Get<IUserProfileRepository>();
        }


    }
}

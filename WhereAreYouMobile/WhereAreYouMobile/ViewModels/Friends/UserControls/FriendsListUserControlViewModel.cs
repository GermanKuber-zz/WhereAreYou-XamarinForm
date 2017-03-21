using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
    public class FriendsListUserControlViewModel : BaseViewModel
    {
        #region Services

        private readonly IFriendsManageService _friendsManageService;
        private readonly IAlertService _alertService;

        #endregion

        #region Properties

        public ObservableCollection<UserProfile> UsersFound { get; set; } = new ObservableCollection<UserProfile>();
        private ObservableCollection<UserProfile> TmpUsersFound { get; set; } = new ObservableCollection<UserProfile>();
        private string _info;
        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;

                this.UsersFound.Clear();
                var info = value.ToLower();

                var list = this.TmpUsersFound.Where(x => x.Email.ToLower().Contains(info)
                || x.DisplayName.ToLower().Contains(info)
                || x.FirstName.ToLower().Contains(info)
                   || x.LastName.ToLower().Contains(info))?.Select(s => s);

                foreach (var item in list)
                    this.UsersFound.Add(item);


                OnPropertyChanged();
            }
        }
        private FriendRequest _userSelected;


        public FriendRequest UserSelected
        {
            get { return _userSelected; }
            set
            {
                _userSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region  Commands
        public ICommand DeleteFriendCommand
        {
            get
            {
                return new Command(async (profileDelete) =>
                {
                    await this.CallWithLoadingAsync(async () =>
                    {
                        var response = await _alertService.DisplayAlertAsync(
                            $"Esta seguro que quiere eliminar a {((UserProfile)profileDelete).DisplayName}",
                            "Eliminar Usuario", "Aceptar", "Cancelar");
                        if (response)
                        {
                            await _friendsManageService.DeleteFriend((UserProfile)profileDelete);
                            await LoadDataAsync();
                        }
                    });
                });
            }
        }

        #endregion

        public FriendsListUserControlViewModel()
        {
            _friendsManageService = DependencyService.Get<IFriendsManageService>();
            _alertService = DependencyService.Get<IAlertService>();
            //MessagingCenter.Subscribe<InvitationReceivedUserControlViewModel>(this, "Hi", (sender) =>
            //                {
            //                    var a = "";
            //                });
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                await this.CallWithLoadingAsync(async () =>
                {
                    var list = await _friendsManageService.GetAllFriendsAsync();
                    this.UsersFound.Clear();
                    this.TmpUsersFound.Clear();
                    foreach (var item in list)
                    {
                        this.UsersFound.Add(item);
                        TmpUsersFound.Add(item);
                    }
                });
            }
            catch (Exception e)
            {
                await LoggerService.LogErrorAsync(e);
                throw;
            }
        }
    }
}

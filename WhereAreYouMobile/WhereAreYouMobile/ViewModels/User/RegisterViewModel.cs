using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.User
{
    public class RegisterViewModel : BaseViewModel
    {

        private readonly ILoginService _loginService;
        private readonly INavigationService _navigation;
        public ICommand ClearCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.IsLoading = true;
                    this.NewUser.Clear();
                    this.IsLoading = false;
                });
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsLoading = true;
                    var response = await _loginService.SignupAsync(NewUser.Email, NewUser.Password, new UserProfile
                    {
                        //TODO: utilizar automapper
                        Email = NewUser.Email,
                        DisplayName = NewUser.DisplayName,
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName
                    });
                    var alertService = DependencyService.Get<IAlertService>();
                    if (response.HasFlag(SignupResponseEnum.EmailExist))
                        await alertService.DisplayAlertAsync("El Email Existe");
                    if (response.HasFlag(SignupResponseEnum.NeedEmailConfirmation))
                    {
                        await alertService.DisplayAlertAsync($"Verifique el correo : {NewUser.Email}, y active su cuenta!!");
                        await _navigation.PopAsync();
                    }
                    this.IsLoading = false;
                });
            }
        }

        private RegisterUserModel _newUser;


        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public RegisterUserModel NewUser
        {
            get { return _newUser; }
            set
            {
                _newUser = value;
                OnPropertyChanged();
            }
        }
        public RegisterViewModel()
        {
            _navigation = DependencyService.Get<INavigationService>();
            _loginService = DependencyService.Get<ILoginService>();
            this.NewUser = new RegisterUserModel();
        }
    }
}

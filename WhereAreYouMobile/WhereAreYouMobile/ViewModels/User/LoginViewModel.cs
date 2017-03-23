using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Views;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.User
{

    public class LoginViewModel : BaseViewModel
    {
        #region Services

        private readonly INavigationService _navigation;
        private readonly IIdentityService _identityService;
        private readonly IExternaLoginService _externaLoginService;

        #endregion


        #region  Commands

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsBusy = true;
                    var loginService = DependencyService.Get<ILoginService>();
                    var response = await loginService.LoguinAsync(Email, Password);
                   
                    var alertService = DependencyService.Get<IAlertService>();

                    if (response != LoginResponseEnum.Ok)
                        this.IsBusy = false;

                    if (response == LoginResponseEnum.VerifyData)
                        await alertService.DisplayAlertAsync("Los datos ingresados son Incorrectos");

                    if (response == LoginResponseEnum.NeedEmailConfirmation)
                        await alertService.DisplayAlertAsync("Debe verificar su email antes de loguearse a la Aplicación");
                    if (response == LoginResponseEnum.Error)
                    {
                       
                        await alertService.DisplayAlertAsync(
                            "Error en el inicio de sesión, verifique con el Administrador");
                    }
                    else
                    {
                        if (response == LoginResponseEnum.Ok)
                           await _navigation.NavigateAsync(new DashBoardPage());
                    }
                
                });
            }
        }
        public ICommand LoginFacebookCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsBusy = true;
                    await _externaLoginService.Login(ExternalLoginTypeEnum.Facebook);
                });
            }
        }
        public ICommand LoginGmailCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsBusy = true;
                    await _externaLoginService.Login(ExternalLoginTypeEnum.Gmail);
                });
            }
        }
        public ICommand LoginTwitterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsBusy = true;
                    await _externaLoginService.Login(ExternalLoginTypeEnum.Twitter);
                });
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.NavigateAsync(new RegisterPage());
                });
            }
        }
        public ICommand DummyUsersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsBusy = true;
                    var loginService = DependencyService.Get<ILoginService>();
                    await loginService.SignupDummysync();
                    this.IsBusy = false;
                });
            }
        }



        #endregion

        #region Properties
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
        private string _password;
        private readonly ILoginAuth0PageRenderService _loginAuth0PageRenderService;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }



        #endregion

        public LoginViewModel()
        {
            this.IsBusy = true;
            _navigation = DependencyService.Get<INavigationService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _externaLoginService = DependencyService.Get<IExternaLoginService>();
            _loginAuth0PageRenderService = DependencyService.Get<ILoginAuth0PageRenderService>();

        }

        public async Task OnAppearingAsync()
        {
            //var delay = Task.Delay(10).ContinueWith(_ =>
            //{
            _loginAuth0PageRenderService.EnableLoginPage();
            var identityService = DependencyService.Get<IIdentityService>();
            var navigation = DependencyService.Get<INavigationService>();
            if (await identityService.IsAuthenticatedAsync())
            {
                await navigation.NavigateAsync(new DashBoardPage());
                this.IsBusy = false;
            }
            else
            {
                this.IsBusy = false;
            }
            //});

        }
    }
}

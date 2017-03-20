using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppService))]
namespace WhereAreYouMobile.Services
{
    public class AppService : IAppService
    {
        private INavigationService _navigation;
        private IIdentityService _identityService;
       

        public async Task StartAsync(Application app)
        {
            _navigation = DependencyService.Get<INavigationService>();
            _identityService = DependencyService.Get<IIdentityService>();
           
            _navigation.SetMainPage(app);
            //if (await _identityService.IsAuthenticatedAsync())
            //{

            //    _navigation.NavigateAsync(new DashBoardPage());
            //}
            //else
            //{
            //    app.MainPage = new NavigationPage(new LoginPage());
            //}
            app.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExternaLoginService))]
namespace WhereAreYouMobile.Services
{
    public class ExternaLoginService : IExternaLoginService
    {
        private readonly INavigationService _navigationService;
        private readonly IExternalLoginConfigurationService _externalLoginConfigurationService;


        public ExternaLoginService()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _externalLoginConfigurationService = DependencyService.Get<IExternalLoginConfigurationService>();
        }
        public async Task<bool> Login(ExternalLoginTypeEnum externalLoginType)
        {
            _externalLoginConfigurationService.SetLoginType(externalLoginType);
           await  _navigationService.NavigateAsync(new LoginExternalPage());
            return true;
        }

    }
}

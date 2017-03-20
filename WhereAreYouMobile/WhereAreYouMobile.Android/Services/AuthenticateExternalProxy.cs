using System;
using System.Threading.Tasks;
using Auth0.SDK;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Droid.Services;
using WhereAreYouMobile.Services.Common;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticateExternalProxy))]
namespace WhereAreYouMobile.Droid.Services
{
    public class AuthenticateExternalProxy : IAuthenticateExternalProxy
    {
        public async Task<UserLogin> Login(string username, string password)
        {
            UserLogin user = null;
            var externalLoginConfigurationService = DependencyService.Get<IExternalLoginConfigurationService>();

            try
            {
                var auth0 = new Auth0Client(externalLoginConfigurationService.Auth0Domain,
                    externalLoginConfigurationService.Auth0ClientId);

                if (auth0 != null)
                {

                    var aDFSUser = await auth0.LoginAsync(externalLoginConfigurationService.Auth0ConnectionName, username, password);

                    if (aDFSUser != null)
                    {
                        Console.WriteLine("Logged in as " + aDFSUser.Profile["name"]);
                    }
                }

                return user;
            }
            catch (TaskCanceledException ex)
            {
                var navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return null;}
    }
}

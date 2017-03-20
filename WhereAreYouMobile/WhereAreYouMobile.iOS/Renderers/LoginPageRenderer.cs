using System;
using System.Threading.Tasks;
using Auth0.SDK;
using Newtonsoft.Json.Linq;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Extensions;
using WhereAreYouMobile.iOS.Renderers;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Views;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginExternalPage), typeof(LoginPageRenderer))]

namespace WhereAreYouMobile.iOS.Renderers
{
    public class LoginPageRenderer : PageRenderer
    {
        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            try
            {


                var loginAuth0PageRenderService = DependencyService.Get<ILoginAuth0PageRenderService>();

                await loginAuth0PageRenderService.LoginWrapper(async () =>
                 {
                     var externalLoginConfigurationService = DependencyService.Get<IExternalLoginConfigurationService>();
                     if (externalLoginConfigurationService == null)
                         throw new ArgumentNullException(nameof(externalLoginConfigurationService));


                     var auth0 = new Auth0Client(externalLoginConfigurationService.Auth0Domain,
                         externalLoginConfigurationService.Auth0ClientId);

                     var user = await auth0.LoginAsync(ViewController, externalLoginConfigurationService.Auth0ConnectionName,
                         false);
                     if (user != null)
                     {
                         Console.WriteLine("Logged in as " + user.Profile["name"]);
                         try
                         {
                             var userLogin = new UserLogin
                             {
                                 UserProfile = new UserProfile
                                 {
                                     DisplayName = user.Profile.GetKeyValue("name"),
                                     Image = user.Profile.GetKeyValue("picture_large"),
                                     Email = user.Profile.GetKeyValue("email"),
                                     Facebook = user.Profile.GetKeyValue("link"),
                                     FirstName = user.Profile.GetKeyValue("given_name"),
                                     LastName = user.Profile.GetKeyValue("family_name")
                                 },
                                 Auth0AccessToken = user.Auth0AccessToken,
                                 RefreshToken = user.RefreshToken,
                                 IdToken = user.IdToken,
                                 DateLogin = DateTime.Now
                             };
                             return userLogin;
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine(e);
                             throw;
                         }
                     }
                     return null;
                 });
            }
            catch (Exception e)
            {

            }

        }


    }
}



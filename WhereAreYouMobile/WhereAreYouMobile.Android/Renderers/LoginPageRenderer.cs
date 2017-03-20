using System;
using Auth0.SDK;
using Newtonsoft.Json.Linq;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Droid.Renderers;
using WhereAreYouMobile.Extensions;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Views;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginExternalPage), typeof(LoginPageRenderer))]
namespace WhereAreYouMobile.Droid.Renderers
{
	public class LoginPageRenderer : PageRenderer
	{
		public LoginPageRenderer()
		{
			ShowLoginPage();
		}

		async void ShowLoginPage()
		{
            var loginAuth0PageRenderService = DependencyService.Get<ILoginAuth0PageRenderService>();
		    await loginAuth0PageRenderService.LoginWrapper(async () =>
		    {
                var externalLoginConfigurationService = DependencyService.Get<IExternalLoginConfigurationService>();
                if (externalLoginConfigurationService == null)
                    throw new ArgumentNullException(nameof(externalLoginConfigurationService));
                var auth0 = new Auth0Client(externalLoginConfigurationService.Auth0Domain, externalLoginConfigurationService.Auth0ClientId);

		        if (auth0 != null)
		        {
		            var user = await auth0.LoginAsync(Context, externalLoginConfigurationService.Auth0ConnectionName, true);
                    
		           
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
                }
                return null;
		    });
		}
        
    }
}


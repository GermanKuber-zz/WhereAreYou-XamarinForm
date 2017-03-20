using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;
using WhereAreYouMobile.Views;
using Xamarin.Forms;
[assembly: Dependency(typeof(LoginAuth0PageRenderService))]
namespace WhereAreYouMobile.Services
{
    public class LoginAuth0PageRenderService : ILoginAuth0PageRenderService
    {
        public LoginAuth0PageRenderService()
        {
            _loggerService = DependencyService.Get<ILoggerService>();
        }
        private bool _run = false;
        private ILoggerService _loggerService;

        public void EnableLoginPage()
        {
            _run = false;
        }

        public async Task LoginWrapper(Func<Task<UserLogin>> action)
        {
            try

            {
                if (!_run)
                {
                    var user = await action();
                    _run = true;
                    var loginService = DependencyService.Get<ILoginService>();
                    if (user != null)
                        await loginService.LoguinAsync(user.UserProfile.Email, user.UserProfile);
                }

            }
            catch (TaskCanceledException ex)
            {
                var navigationService = DependencyService.Get<INavigationService>();
                await navigationService.PopAsync();

            }
            catch (Exception ex)
            {
                await _loggerService.LogErrorAsync(ex);

            }
        }
    }
}
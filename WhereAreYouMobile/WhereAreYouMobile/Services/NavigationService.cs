using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace WhereAreYouMobile.Services
{
    public class NavigationService : INavigationService
    {
        private MasterDetailPage _navigationMasterPage;
        private NavigationPage _navigationPage;
        private List<Page> _pages = new List<Page>();

        private Application _mainPage;
        private readonly IIdentityService _identityService;
        public NavigationService()
        {

            _identityService = DependencyService.Get<IIdentityService>();
        }


        public void SetNavigationPage(NavigationPage navigationPage)
        {
            _navigationPage = navigationPage;
        }
        public void SetRootPage(MasterDetailPage navigation)
        {
            _navigationMasterPage = navigation;
        }

        public async Task PopAsync()
        {
            if (this._mainPage != null)
            {

                if (this._mainPage?.MainPage.GetType() == typeof(NavigationPage))
                {

                    await ((NavigationPage)this._mainPage.MainPage).PopAsync(true);
                }

            }
        }

        public async Task NavigateAsync(Page page)
        {
            if (this._mainPage != null)
            {
                if (!(await _identityService.IsAuthenticatedAsync()))
                {
                    if (this._mainPage?.MainPage.GetType() != typeof(NavigationPage))
                    {
                        var navigationPage = new NavigationPage(page);
                        this._mainPage.MainPage = navigationPage;
                    }
                    else
                    {
                        await ((NavigationPage)this._mainPage.MainPage).PushAsync(page, true);
                    }
                }
                else
                {
                    if (this._mainPage.MainPage == null ||
                        this._mainPage?.MainPage.GetType() != typeof(MasterPageContainer))
                    {

                        this._mainPage.MainPage = new MasterPageContainer();
                    }
                    else
                    {
                        if (((MasterPageContainer)this._mainPage.MainPage).Detail != null &&
                            Device.OS == TargetPlatform.WinPhone)
                        {
                            await this._navigationMasterPage.Detail.Navigation.PopToRootAsync();
                        }
                        ((MasterPageContainer)this._mainPage.MainPage).Detail = new NavigationPage(page);

                        ((MasterPageContainer)this._mainPage.MainPage).IsPresented = false;
                    }
                }
            }
            else
            {
                throw new Exception("La propiedad appMainPage es null");
            }
        }

        public void SetMainPage(Application appMainPage)
        {
            this._mainPage = appMainPage;
        }
    }

}

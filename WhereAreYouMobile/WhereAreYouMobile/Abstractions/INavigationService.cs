using System.Threading.Tasks;
using WhereAreYouMobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace WhereAreYouMobile.Abstractions
{

    public interface INavigationService
    {
        Task NavigateAsync(Page page);
        void SetMainPage(Application appMainPage);
        void SetNavigationPage(NavigationPage navigationPage);
        void SetRootPage(MasterDetailPage navigation);
        Task PopAsync();
    }
}

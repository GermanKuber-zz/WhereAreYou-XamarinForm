using System.Threading.Tasks;
using Xamarin.Forms;

namespace WhereAreYouMobile.Abstractions
{
    public interface IAppService
    {
        Task StartAsync(Application app);
    }
}
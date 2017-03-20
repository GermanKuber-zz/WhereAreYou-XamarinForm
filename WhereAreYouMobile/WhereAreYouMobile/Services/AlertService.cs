using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(AlertService))]
namespace WhereAreYouMobile.Services
{
    public class AlertService : IAlertService
    {
        public async Task DisplayAlertAsync(string message, string title = "Información", string buttonText = "OK")
        {
           await Application.Current.MainPage.DisplayAlert(message, title, buttonText);
        }
    }
}
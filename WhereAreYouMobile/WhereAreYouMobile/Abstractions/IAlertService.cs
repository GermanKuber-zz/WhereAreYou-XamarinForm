using System.Threading.Tasks;

namespace WhereAreYouMobile.Abstractions
{
    public interface IAlertService
    {
        Task DisplayAlertAsync(string message, string title = "Información", string buttonText = "OK");
    }
}
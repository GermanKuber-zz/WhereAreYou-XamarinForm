using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoggerService))]
namespace WhereAreYouMobile.Services
{
    public class LoggerService : ILoggerService
    {
        public Task LogErrorAsync(Exception exception)
        {
            return null;
        }
    }
}
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using Xamarin.Forms;
using System;
[assembly: Dependency(typeof(EventAgregatorService))]
namespace WhereAreYouMobile.Services
{
	public class EventAgregatorService
	{
		public void Subscribe(EventAgregatorTypeEnum eventType, Type typeParameter)
		{
			MessagingCenter.Subscribe<EventAgregatorService, string>(this, "Hi", (sender, arg) =>
			{

			});
		}
	}

	public enum EventAgregatorTypeEnum
	{
	}
}
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using Xamarin.Forms;
using System;
using WhereAreYouMobile.Services.Common;

[assembly: Dependency(typeof(EventAgregatorService))]
namespace WhereAreYouMobile.Services
{
	public class EventAgregatorService : IEventAgregatorService
    {
        private readonly IIdentityService _identityService;

        public EventAgregatorService()
        {
            _identityService = DependencyService.Get<IIdentityService>();
        }
        public void Subscribe(EventAgregatorTypeEnum eventType, Action callBack)
        {
            if (callBack == null)
                throw new ArgumentNullException(nameof(callBack));
            MessagingCenter.Subscribe<EventAgregatorService>(this, eventType.ToString(), (sender) =>
            {
                callBack.Invoke();

            });
        }
        public void Subscribe<TArgument>(EventAgregatorTypeEnum eventType, Action<TArgument> callBack)
        {
            if (callBack == null)
                throw new ArgumentNullException(nameof(callBack));
            MessagingCenter.Subscribe<EventAgregatorService, TArgument>(this, eventType.ToString(), (sender, arg) =>
            {
                callBack.Invoke(arg);
            });
        }
        public void Raise<TArgument>(EventAgregatorTypeEnum eventType, TArgument parameter )
        {
            MessagingCenter.Send<EventAgregatorService,TArgument>(this, eventType.ToString(), parameter);
        }
        public void Raise(EventAgregatorTypeEnum eventType)
        {
            MessagingCenter.Send<EventAgregatorService>(this, eventType.ToString());
        }
    }
}
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

        public void SubscribeRemotes(EventAgregatorRemoteEnum eventType,string remoteId, Action callBack)
        {
            if (callBack == null)
                throw new ArgumentNullException(nameof(callBack));
            MessagingCenter.Subscribe<EventAgregatorService>(this, eventType.ToString(), (sender) =>
            {
                callBack.Invoke();
            });
        }
        public void SubscribeRemote<TArgument>(EventAgregatorRemoteEnum eventType, string remoteId, Action<TArgument> callBack)
        {
            if (callBack == null)
                throw new ArgumentNullException(nameof(callBack));
            MessagingCenter.Subscribe<EventAgregatorService, TArgument>(this, eventType.ToString(), (sender, arg) =>
            {
                callBack.Invoke(arg);
            });
        }
        public void Subscribe(EventAgregatorRemoteEnum eventType, Action callBack)
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
        public void Raise<TArgument>(EventAgregatorTypeEnum eventType, TArgument parameter)
        {
            MessagingCenter.Send<EventAgregatorService, TArgument>(this, eventType.ToString(), parameter);
        }
        public void Raise(EventAgregatorTypeEnum eventType)
        {
            MessagingCenter.Send<EventAgregatorService>(this, eventType.ToString());
        }
        public void RaiseRemote<TArgument>(EventAgregatorRemoteEnum eventType, string remoteId, TArgument parameter)
        {
            MessagingCenter.Send<EventAgregatorService, TArgument>(this, eventType.ToString(), parameter);
        }
        public void RaiseRemote(EventAgregatorRemoteEnum eventType, string remoteId)
        {
            MessagingCenter.Send<EventAgregatorService>(this, eventType.ToString());
        }
    }
}
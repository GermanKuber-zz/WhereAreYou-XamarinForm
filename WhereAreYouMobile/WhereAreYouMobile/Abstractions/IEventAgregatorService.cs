using System;
using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface IEventAgregatorService
    {
        void Raise(EventAgregatorTypeEnum eventType);
        void Raise<TArgument>(EventAgregatorTypeEnum eventType, TArgument parameter);
        void RaiseRemote(EventAgregatorRemoteEnum eventType, string remoteId);
        void RaiseRemote<TArgument>(EventAgregatorRemoteEnum eventType, string remoteId, TArgument parameter);
        void Subscribe(EventAgregatorRemoteEnum eventType, Action callBack);
        void Subscribe<TArgument>(EventAgregatorTypeEnum eventType, Action<TArgument> callBack);
        void SubscribeRemote<TArgument>(EventAgregatorRemoteEnum eventType, string remoteId, Action<TArgument> callBack);
        void SubscribeRemotes(EventAgregatorRemoteEnum eventType, string remoteId, Action callBack);
    }
}
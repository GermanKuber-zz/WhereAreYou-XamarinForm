using System;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface IEventAgregatorService
    {
        void Raise(EventAgregatorTypeEnum eventType);
        void Raise<TArgument>(EventAgregatorTypeEnum eventType, TArgument parameter);
        void Subscribe(EventAgregatorTypeEnum eventType, Action callBack);
        void Subscribe<TArgument>(EventAgregatorTypeEnum eventType, Action<TArgument> callBack);
    }
}
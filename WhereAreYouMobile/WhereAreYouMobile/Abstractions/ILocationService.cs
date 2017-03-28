using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Services;

namespace WhereAreYouMobile.Abstractions
{
    public interface ILocationService
    {
        Task UpdateMeLocation(Action<LocationPosition> callBack);
        Task<LocationPosition> GetCurrentPosition();
    }
}
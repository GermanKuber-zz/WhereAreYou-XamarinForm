using WhereAreYouMobile.ViewModels.Map;

namespace WhereAreYouMobile.Abstractions
{
    public interface IMapService
    {
        void SetMap(CustomMap customMap);
        void MoveToRegion(double latitude, double longitude);
    }
}
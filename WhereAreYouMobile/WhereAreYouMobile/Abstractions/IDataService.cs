using Microsoft.WindowsAzure.MobileServices;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions
{
    public interface IDataService
    {
        IMobileServiceTable<UserProfile> UserProfileTable { get; }
        IMobileServiceTable<FriendRequest> FriendRequestTable { get; }
        IMobileServiceTable<Friend> Friends { get; }
        
    }

    public interface IDataServiceSync
    {

    }

}
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;
using Xamarin.Forms;
using System.Linq;
//#define OFFLINE_SYNC_ENABLED
[assembly: Dependency(typeof(DataService))]
namespace WhereAreYouMobile.Services
{
    public class DataService : IDataService
    {

        // sync tables
        private IMobileServiceTable<UserProfile> _userProfileTable;
        public IMobileServiceTable<UserProfile> UserProfileTable
        {
            get
            {
                Init().Wait();
                return _userProfileTable;
            }
        }
        private IMobileServiceTable<FriendRequest> _friendRequestTable;
        public IMobileServiceTable<FriendRequest> FriendRequestTable
        {
            get
            {
                Init().Wait();
                return _friendRequestTable;
            }
        }
        private IMobileServiceTable<Friend> _friends;
        public IMobileServiceTable<Friend> Friends
        {
            get
            {
                Init().Wait();
                return _friends;
            }
        }
        

        static readonly Lazy<MobileServiceClient> LazyMobileServiceClient =
            new Lazy<MobileServiceClient>(() =>
            {
                string serviceUrl = DependencyService.Get<IConfigFetcher>().GetAsync(ConfigurationsKeyEnum.DataServiceUrl).Result;
                return new MobileServiceClient(serviceUrl);
                //#if DEBUG

                //                // using a special handler on iOS so that we can use the Charles debugging proxy to inspect HTTP traffic

                //                var handlerFactory = DependencyService.Get<IHttpClientHandlerFactory>();

                //                    if (handlerFactory != null)
                //                    {
                //                        return new MobileServiceClient(serviceUrl, handlerFactory.GetHttpClientHandler());
                //                    }

                //                    return new MobileServiceClient(serviceUrl);

                //#else

                //                    return new MobileServiceClient(serviceUrl);

                //#endif
            });

        public static MobileServiceClient AzureAppServiceClient => LazyMobileServiceClient.Value;

        public async Task Init()
        {
            _userProfileTable = AzureAppServiceClient.GetTable<UserProfile>();
            _friendRequestTable = AzureAppServiceClient.GetTable<FriendRequest>();
            _friends = AzureAppServiceClient.GetTable<Friend>();
        }

    }
}


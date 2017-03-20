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
[assembly: Dependency(typeof(DataServiceSync))]
namespace WhereAreYouMobile.Services
{
    public class DataServiceSync : IDataServiceSync
    {

    


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

        public static MobileServiceClient AzureAppServiceClient
        {
            get
            {
                return LazyMobileServiceClient.Value;
            }
        }

        public async Task Init()
        {
            if (LocalDBExists)
                return;

            var store = new MobileServiceSQLiteStore("syncstore.db");

            store.DefineTable<UserProfile>();

            try
            {
                await AzureAppServiceClient.SyncContext.InitializeAsync(store);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Failed to initialize sync context: {0}", ex.Message);
            }

            //_userProfileSyncTable = AzureAppServiceClient.GetSyncTable<UserProfile>();
        }

        #region data seeding and local DB status

        public bool LocalDBExists
        {
            get { return AzureAppServiceClient.SyncContext.IsInitialized; }
        }

        bool _IsSeeded;

        public bool IsSeeded { get { return _IsSeeded; } }

        public async Task SeedLocalDataAsync()
        {
            await Execute(
                async () =>
                {
                    await SynchronizeUserProfilesAsync();
                    _IsSeeded = true;
                }
            );
        }

        #endregion


        #region Orders

        public async Task SynchronizeUserProfilesAsync()
        {
            await Execute(
                async () =>
                {
                    if (!LocalDBExists)
                    {
                        await Init();
                    }

                    //await _userProfileSyncTable.PullAsync("syncUserProfiles", _userProfileSyncTable.CreateQuery());
                }
            );
        }


        #endregion



        #region some nifty helpers

        static async Task Execute(Func<Task> execute)
        {
            try
            {
                await execute();
            }
            // isolate mobile service errors
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"MOBILE SERVICE ERROR {0}", ex.Message);
            }
            // catch all other errors
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        static async Task<T> Execute<T>(Func<Task<T>> execute, T defaultReturnObject)
        {
            try
            {
                return await execute();
            }
            catch (MobileServiceInvalidOperationException ex) // isolate mobile service errors
            {
                Debug.WriteLine(@"MOBILE SERVICE ERROR {0}", ex.Message);
            }
            catch (Exception ex2) // catch all other errors
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return defaultReturnObject;
        }



        #endregion










































        //IMobileServiceTable<UserProfile> _userTable;

        //public DataService()
        //{
        //    try
        //    {
        //        string serviceUrl = DependencyService.Get<IConfigFetcher>().GetAsync("dataServiceUrl").Result;
        //        var client = new MobileServiceClient(serviceUrl);

        //        _userTable = client.GetTable<UserProfile>();
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }

        //}


        //public async Task AddUserProfile(UserProfile userProfile)
        //{
        //    try
        //    {
        //        await this._userTable.InsertAsync(userProfile);
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }

        //}


        //#region some nifty helpers

        //static async Task Execute(Func<Task> execute)
        //{
        //    try
        //    {
        //        await execute();
        //    }
        //    // isolate mobile service errors
        //    catch (MobileServiceInvalidOperationException ex)
        //    {
        //        Debug.WriteLine(@"MOBILE SERVICE ERROR {0}", ex.Message);
        //    }
        //    // catch all other errors
        //    catch (Exception ex2)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", ex2.Message);
        //    }
        //}

        //static async Task<T> Execute<T>(Func<Task<T>> execute, T defaultReturnObject)
        //{
        //    try
        //    {
        //        return await execute();
        //    }
        //    catch (MobileServiceInvalidOperationException ex) // isolate mobile service errors
        //    {
        //        Debug.WriteLine(@"MOBILE SERVICE ERROR {0}", ex.Message);
        //    }
        //    catch (Exception ex2) // catch all other errors
        //    {
        //        Debug.WriteLine(@"ERROR {0}", ex2.Message);
        //    }
        //    return defaultReturnObject;
        //}


        //#endregion
    }
}


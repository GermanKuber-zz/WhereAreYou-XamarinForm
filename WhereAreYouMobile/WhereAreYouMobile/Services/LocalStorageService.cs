using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using RestSharp.Portable;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalStorageService))]
namespace WhereAreYouMobile.Services
{
    public class LocalStorageService : ILocalStorageService
    {

        public LocalStorageService()
        {

        }
        public async Task<bool> InsertObjectSecureAsync<TEntity>(LocalStorageKeys key, TEntity entity)
        {
            try
            {
                //var json = JsonConvert.SerializeObject(entity);
                //await BlobCache.Secure.InsertObject(key.ToString(), json);
                await BlobCache.Secure.InsertObject(key.ToString(), entity);
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<TEntity> GetObjectSecureAsync<TEntity>(LocalStorageKeys key)
        {
            try
            {
                //await BlobCache.Secure.InvalidateAll();
                //var a = await BlobCache.Secure.GetAllKeys();
                //var l = a.ToList();
                var returnData = await BlobCache.Secure.GetObject<TEntity>(key.ToString());
                //if (!string.IsNullOrWhiteSpace(returnJson))
                //{
                //    var output = JsonConvert.DeserializeObject<TEntity>(returnJson);
                //    return output;
                //}
                //await BlobCache.Secure.InvalidateAll();

                return returnData;
                //return default(TEntity);
            }
            catch (KeyNotFoundException e)
            {
                return default(TEntity);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> InsertObjectSettingsAsync<TEntity>(LocalStorageKeys key, TEntity entity)
        {
            try
            {
                await BlobCache.UserAccount.InsertObject(key.ToString(), entity);
                return false;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<TEntity> GetObjectSettingsAsync<TEntity>(LocalStorageKeys key)
        {
            try
            {
                var returnData = await BlobCache.UserAccount.GetObject<TEntity>(key.ToString());
                return returnData;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    public enum LocalStorageKeys
    {
        ProfileLogued,
        IsAuthenticated
    }

}
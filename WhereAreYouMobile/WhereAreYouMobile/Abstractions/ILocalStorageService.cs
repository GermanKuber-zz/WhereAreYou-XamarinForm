using System.Threading.Tasks;
using WhereAreYouMobile.Services;

namespace WhereAreYouMobile.Abstractions
{
    public interface ILocalStorageService
    {
        Task<TEntity> GetObjectSecureAsync<TEntity>(LocalStorageKeys key);
        Task<TEntity> GetObjectSettingsAsync<TEntity>(LocalStorageKeys key);
        Task<bool> InsertObjectSecureAsync<TEntity>(LocalStorageKeys key, TEntity entity);
        Task<bool> InsertObjectSettingsAsync<TEntity>(LocalStorageKeys key, TEntity entity);
    }
}
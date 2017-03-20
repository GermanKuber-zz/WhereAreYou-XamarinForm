using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services.Repositories;
using Xamarin.Forms;
[assembly: Dependency(typeof(UserProfileRepository))]
namespace WhereAreYouMobile.Services.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {

        private readonly IDataService _dataService;
        private readonly ILoggerService _loggerService;

        public UserProfileRepository()
        {
            _dataService = DependencyService.Get<IDataService>();
            _loggerService = DependencyService.Get<ILoggerService>();
        }
        public async Task SaveAsync(UserProfile item)
        {
            try
            {
                if (item.Id == null)
                {
                    var profile = await _dataService.UserProfileTable.Where(x => x.Email.ToUpper() == item.Email.ToUpper()).ToEnumerableAsync();
                    if (profile != null && profile.Any())
                        throw new Exception("Esta intentango agregar un nuevo profile con un email que ya existe");
                    await _dataService.UserProfileTable.InsertAsync(item);
                }
                else
                    await _dataService.UserProfileTable.UpdateAsync(item);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public async Task<UserProfile> GetByEmailAsync(string email)
        {
            try
            {
                var profile = await _dataService.UserProfileTable
                        .Where(x => x.Email.ToUpper() == email.ToUpper()).ToEnumerableAsync();
                foreach (var userProfile in profile)
                    return userProfile;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<UserProfile>> GetUsersByIdList(List<string> ids)
        {
            try
            {
                var profile = await _dataService.UserProfileTable
                        .Where(x => ids.Contains(x.Id)).ToEnumerableAsync();
                return profile;
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
        }
    }
}

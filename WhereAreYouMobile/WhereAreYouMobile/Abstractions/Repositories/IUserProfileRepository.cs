using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Abstractions.Repositories
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetUsersByIdList(List<string> ids);
        Task SaveAsync(UserProfile item);
        Task<UserProfile> GetByEmailAsync(string email);
    }
}
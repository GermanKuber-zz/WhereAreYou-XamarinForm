using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(IdentityService))]
namespace WhereAreYouMobile.Services
{
    public class IdentityService : IIdentityService
    {

        private async Task<bool> GetIsAuthenticatedAsync()
        {
            var isAuthenticated = await _localStorageService.GetObjectSecureAsync<bool>(LocalStorageKeys.IsAuthenticated);
            return isAuthenticated;
        }

        private async Task SetIsAuthenticated(bool value)
        {
            await _localStorageService.InsertObjectSecureAsync<bool>(LocalStorageKeys.IsAuthenticated, value);
        }

        private readonly IUserProfileRepository _userRepository;
        private readonly ILocalStorageService _localStorageService;
        private IConfigFetcher _configFetcher;

        public async Task<UserProfile> GetUserLoguedAsync()
        {
            var profile = await _localStorageService.GetObjectSecureAsync<UserProfile>(LocalStorageKeys.ProfileLogued);
            return profile;
        }

        public async Task SetUserLoguedAsync(UserProfile value)
        {
            await _localStorageService.InsertObjectSecureAsync<UserProfile>(LocalStorageKeys.ProfileLogued, value);
        }

        public IdentityService()
        {
            _userRepository = DependencyService.Get<IUserProfileRepository>();
            _localStorageService = DependencyService.Get<ILocalStorageService>();
            _configFetcher = DependencyService.Get<IConfigFetcher>();
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            return await GetIsAuthenticatedAsync();
        }
        /// <summary>
        /// Se utiliza para registrar al usuario que se registrar por Social Network (Facebbok, Gmail, etc..)
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public async Task<bool> RegisterByExternalAppAsync(UserProfile userProfile)
        {
            userProfile.ExternalAuth = true;
            userProfile.Activate = true;

            await this._userRepository.SaveAsync(userProfile);
            await this.SetIsAuthenticated(true);
            await this.SetUserLoguedAsync(userProfile);
            return await GetIsAuthenticatedAsync();
        }
        /// <summary>
        /// Se utiliza para registrar el usuario que se registrar con correo y contraseña
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(UserProfile userProfile)
        {
            //Registro el perfil del usuario por Email
            userProfile.ExternalAuth = false;
            userProfile.Activate = false;
            await this._userRepository.SaveAsync(userProfile);
            return true;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            await this.SetIsAuthenticated(true);
            return await GetIsAuthenticatedAsync();
        }
        public async Task<bool> LoginByEmailAsync(string email)
        {
            var profile = await _userRepository.GetByEmailAsync(email);
            if (profile != null)
            {
                ////Configuración para aceptar loguin sin mail de confirmación
                //var value = false;
                //Boolean.TryParse(await _configFetcher.GetAsync(ConfigurationsKeyEnum.EnableMailConfirmation), out value);
                //if (value)
                    if (!profile.Activate)
                        await this.ActivateProfileAsync(profile);
                await this.SetIsAuthenticated(profile != null);
                if (await GetIsAuthenticatedAsync())
                    await this.SetUserLoguedAsync(profile);
            }
            else
            {
                await this.SetIsAuthenticated(false);
            }
            return await GetIsAuthenticatedAsync();
        }
        public async Task ActivateProfileAsync(UserProfile userProfile)
        {
            try
            {
                userProfile.Activate = true;
                await _userRepository.SaveAsync(userProfile);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task ActivateUserByEmailAsync(string email)
        {
            try
            {
                var profile = await _userRepository.GetByEmailAsync(email);
                profile.Activate = true;
                await _userRepository.SaveAsync(profile);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> LogOutAsync()
        {
            await this.SetIsAuthenticated(false);
            await this.SetUserLoguedAsync(null);
            return await GetIsAuthenticatedAsync();
        }
    }
}

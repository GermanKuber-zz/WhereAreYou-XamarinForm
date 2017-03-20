using System;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Services.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExternalLoginConfigurationService))]
namespace WhereAreYouMobile.Services
{
    public class ExternalLoginConfigurationService : IExternalLoginConfigurationService
    {
        private ExternalLoginTypeEnum _externalLoginType;
        private bool _isSetExternalType = false;
        private readonly IConfigFetcher _configurationService;

        #region  Public Properties

        public string Auth0Domain => _configurationService.GetAsync(ConfigurationsKeyEnum.Auth0Domain, true).Result;
        public string Auth0ClientId => _configurationService.GetAsync(ConfigurationsKeyEnum.Auth0ClientId, true).Result;
        public string Auth0DbConnections => _configurationService.GetAsync(ConfigurationsKeyEnum.Auth0DbConnections, true).Result;
        public string Auth0DbConnectionsSignUp => _configurationService.GetAsync(ConfigurationsKeyEnum.Auth0DbConnectionsSignUp, true).Result;
        public string Auth0DbConnectionsLogin => _configurationService.GetAsync(ConfigurationsKeyEnum.Auth0DbConnectionsLogin, true).Result;


        public string Auth0ConnectionName
        {
            get
            {
                if (!_isSetExternalType)
                    throw new Exception("Intenta hacer login externo sin seter el tipo");
                switch (_externalLoginType)
                {
                    case ExternalLoginTypeEnum.Facebook:
                        return "facebook";
                    case ExternalLoginTypeEnum.Gmail:
                        return "google-oauth2";
                    case ExternalLoginTypeEnum.Twitter:
                        return "twitter";
                    case ExternalLoginTypeEnum.WindowsLive:
                        return "windowslive";
                    case ExternalLoginTypeEnum.UsernamePassword:
                        return "Username-Password-Authentication";
                    default:
                        return "";
                }
            }
        }

        #endregion



        public ExternalLoginConfigurationService()
        {
            _configurationService = DependencyService.Get<IConfigFetcher>();
        }

        public void SetLoginType(ExternalLoginTypeEnum externalLoginType)
        {
            _externalLoginType = externalLoginType;
            _isSetExternalType = true;
        }

    }

}


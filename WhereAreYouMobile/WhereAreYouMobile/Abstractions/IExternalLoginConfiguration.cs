using WhereAreYouMobile.Services.Common;

namespace WhereAreYouMobile.Abstractions
{
    public interface IExternalLoginConfigurationService
    {
        void SetLoginType(ExternalLoginTypeEnum externalLoginType);
        string Auth0ConnectionName { get; }
        string Auth0Domain { get; }
        string Auth0ClientId { get; }
        string Auth0DbConnections { get; }
        string Auth0DbConnectionsSignUp { get; }
        string Auth0DbConnectionsLogin { get; }


    }
}
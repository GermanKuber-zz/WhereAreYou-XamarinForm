using System.Threading.Tasks;

namespace WhereAreYouMobile.Abstractions.Config
{
    public interface IConfigFetcher
    {
        Task<string> GetAsync(ConfigurationsKeyEnum configElementName, bool readFromSensitiveConfig = false);
    }
    public enum ConfigurationsKeyEnum
    {
        DataServiceUrl,
        Auth0Domain,
        Auth0ClientId,
        Auth0DbConnections,
        Auth0DbConnectionsSignUp,
        Auth0DbConnectionsLogin,
        //Si esta activado requiere confirmación por email el login 
        EnableMailConfirmation
    }
}
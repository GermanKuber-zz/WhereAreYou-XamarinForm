using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp.Portable;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views.User;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginService))]
namespace WhereAreYouMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly IIdentityService _identityService;
        private readonly INavigationService _navigationService;
        private readonly IExternalLoginConfigurationService _externalLoginConfigurationService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILoggerService _loggerService;

        public LoginService()
        {
            _identityService = DependencyService.Get<IIdentityService>();
            _navigationService = DependencyService.Get<INavigationService>();
            _userProfileRepository = DependencyService.Get<IUserProfileRepository>();
            _externalLoginConfigurationService = DependencyService.Get<IExternalLoginConfigurationService>();
            _loggerService = DependencyService.Get<ILoggerService>();
        }
        public async Task<LoginResponseEnum> LoguinAsync(string email, string password)
        {
            try
            {
                var client =
                    new RestSharp.Portable.HttpClient.RestClient(
                        $"{_externalLoginConfigurationService.Auth0DbConnections}{_externalLoginConfigurationService.Auth0DbConnectionsLogin}");
                var request = new RestRequest(Method.POST);
                client.IgnoreResponseStatusCode = true;
                var root = new LoginAuth0User
                {
                    UserName = email,
                    Password = password,
                    ClientId = _externalLoginConfigurationService.Auth0ClientId,
                    Connection = "Username-Password-Authentication"
                };
                string json = JsonConvert.SerializeObject(root);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(root);
                var response = await client.Execute(request);
                if (response.IsSuccess)
                {

                    var identityResponse = await this._identityService.LoginByEmailAsync(email);
                    return LoginResponseEnum.Ok;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ResponseError>(response.Content);
                    if (error.Error == "invalid_user_password" || error.Error == "invalid_request")
                        return LoginResponseEnum.VerifyData;
                    if (error.Error == "unauthorized")
                        return LoginResponseEnum.NeedEmailConfirmation;
                }
            }
            catch (Exception e)
            {
                await _loggerService.LogErrorAsync(e);
                throw;
            }
            return LoginResponseEnum.Error;
        }
        public async Task<bool> LoguinAsync(string email, UserProfile userProfile)
        {
            var loginUser = await _identityService.LoginByEmailAsync(email);
            if (!loginUser)
            {
                //await _navigationService.NavigateAsync(new ConfirmRegisterExternalApp());
                //Si el usuario no se logueo
                if (await _identityService.RegisterByExternalAppAsync(userProfile))
                {
                    await _navigationService.PopAsync();
                    await _navigationService.PopAsync();
                    await _navigationService.PopAsync();
                    await _navigationService.PopAsync();
                    //await _navigationService.NavigateAsync(new ConfirmRegisterExternalApp());
                    //await _navigationService.NavigateAsync(new DashBoardPage());
                    return true;
                }
            }
            else
            {
                await _navigationService.PopAsync();
                //await _navigationService.NavigateAsync(new DashBoardPage());
                return true;
            }
            return false;
        }
        public Task<bool> LoguinAsync(UserProfile userProfile)
        {
            return Task<bool>.Factory.StartNew(() => true);
        }
        public async Task<SignupResponseEnum> SignupAsync(string email, string password, UserProfile userProfile)
        {
            try
            {
                var profile = await _userProfileRepository.GetByEmailAsync(email);
                if (profile == null)
                {

                    var client =
                        new RestSharp.Portable.HttpClient.RestClient(
                            $"{_externalLoginConfigurationService.Auth0DbConnections}{_externalLoginConfigurationService.Auth0DbConnectionsSignUp}");
                    var request = new RestRequest(Method.POST);
                    client.IgnoreResponseStatusCode = true;
                    var root = new SignUpAuth0User(email,
                        password,
                        _externalLoginConfigurationService.Auth0ClientId,
                        "Username-Password-Authentication"
                    );
                    string json = JsonConvert.SerializeObject(root);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddJsonBody(root);
                    var response = await client.Execute(request);

                    if (response.IsSuccess)
                    {
                        var identityResponse = await this._identityService.RegisterAsync(userProfile);
                        return SignupResponseEnum.NeedEmailConfirmation;
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<ResponseError>(response.Content);
                        if (error.Error == "invalid_user_password" || error.Error == "invalid_request")
                            return SignupResponseEnum.EmailExist;
                        //if (error.Error == "unauthorized")
                        //    return SignupResponseEnum.NeedEmailConfirmation;
                    }
                    return SignupResponseEnum.Registered | SignupResponseEnum.NeedEmailConfirmation;
                }
                else
                {
                    return SignupResponseEnum.EmailExist;
                }
            }
            catch (Exception ex)
            {
                await _loggerService.LogErrorAsync(ex);
                throw;
            }

        }

        public async Task SignupDummysync()
        {
            try
            {

                var clientJson =
                new RestSharp.Portable.HttpClient.RestClient(
                    $"http://beta.json-generator.com/api/json/get/Nk1dgANsf");
                var requestJson = new RestRequest(Method.GET);
                clientJson.IgnoreResponseStatusCode = true;

                var responseJson = await clientJson.Execute(requestJson);

                var responseParse = JsonConvert.DeserializeObject<List<UserProfile>>(responseJson.Content);
                foreach (var userProfile in responseParse)
                {
                    var client =
                        new RestSharp.Portable.HttpClient.RestClient(
                            $"{_externalLoginConfigurationService.Auth0DbConnections}{_externalLoginConfigurationService.Auth0DbConnectionsSignUp}");
                    var request = new RestRequest(Method.POST);
                    client.IgnoreResponseStatusCode = true;
                    var root = new SignUpAuth0User(userProfile.Email,
                        "Password",
                        _externalLoginConfigurationService.Auth0ClientId,
                        "Username-Password-Authentication"
                    );
                    string json = JsonConvert.SerializeObject(root);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddJsonBody(root);
                    var response = await client.Execute(request);

                    if (response.IsSuccess)
                    {
                        var identityResponse = await this._identityService.RegisterAsync(userProfile);

                    }



                }
            }
            catch (Exception ex)
            {
                await _loggerService.LogErrorAsync(ex);
                throw;
            }

        }
    }

    public enum SignupResponseEnum
    {
        Registered = 0,
        NeedEmailConfirmation = 1,
        EmailExist = 2,
        PasswordError = 4,
        EmailIncorrect = 8
    }
    public enum LoginResponseEnum
    {
        NeedEmailConfirmation = 0,
        VerifyData = 1,
        Error = 2,
        Ok = 3
    }

    public class ResponseError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
    public class UserMetadata
    {

    }

    public class LoginAuth0User
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("connection")]
        public string Connection { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }

    public class SignUpAuth0User
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("connection")]
        public string Connection { get; set; }
        [JsonProperty("eighty_min_score")]
        public UserMetadata UserMetadata { get; set; }

        public SignUpAuth0User(string email, string password, string clientId, string connection)
        {
            this.ClientId = ClientId;
            this.Email = email;
            this.Password = password;
            this.Connection = connection;
        }
    }

}
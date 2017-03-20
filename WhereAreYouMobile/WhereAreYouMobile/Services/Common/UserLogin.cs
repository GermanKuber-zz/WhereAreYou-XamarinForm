using System;
using WhereAreYouMobile.Data;

namespace WhereAreYouMobile.Services.Common
{
    public class UserLogin
    {
        public string Auth0AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string IdToken { get; set; }
        public UserProfile UserProfile { get; set; }
        public DateTime DateLogin { get; set; }
        public DateTime DateRefresh { get; set; }
        public UserLogin()
        {
            this.UserProfile = new UserProfile();
        }
    }
}
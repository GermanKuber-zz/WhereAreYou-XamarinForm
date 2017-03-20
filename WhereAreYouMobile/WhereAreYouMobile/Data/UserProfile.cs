using WhereAreYouMobile.Views;

namespace WhereAreYouMobile.Data
{
    public class UserProfile : BaseModel
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Linkedin { get; set; } = string.Empty;
        public bool Activate { get; set; } = false;
        public bool ExternalAuth { get; set; }
    }
}

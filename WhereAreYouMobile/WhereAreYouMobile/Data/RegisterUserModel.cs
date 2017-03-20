namespace WhereAreYouMobile.Data
{
    public class RegisterUserModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public void Clear()
        {
            this.Email = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.DisplayName = string.Empty;
            this.Password = string.Empty;
            this.RePassword = string.Empty;
        }
    }
}
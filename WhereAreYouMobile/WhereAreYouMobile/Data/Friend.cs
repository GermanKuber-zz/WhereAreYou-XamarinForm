using WhereAreYouMobile.Views;

namespace WhereAreYouMobile.Data
{
    public class Friend : BaseModel
    {
        public string IdUser { get; set; } = string.Empty;
        public string IdFriend { get; set; } = string.Empty;
        public Friend DateCreate { get; set; }
    }
}
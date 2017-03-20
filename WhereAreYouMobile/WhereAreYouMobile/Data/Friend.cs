using System;
using WhereAreYouMobile.Views;

namespace WhereAreYouMobile.Data
{
    public class Friend : BaseModel
    {
        public string IdUser { get; set; } = string.Empty;
        public string IdFriend { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; }

        public Friend(string idUser, string idFriend) {
            this.IdFriend = IdFriend;
            this.IdUser = IdUser;
            this.DateCreate = DateTime.Now;
        }
    }
}
using System;
using WhereAreYouMobile.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Abstractions.Repositories;
using WhereAreYouMobile.Data;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;

namespace WhereAreYouMobile.Data
{
    public class FriendRequest : BaseModel
    {
        public string IdUserSendInvitation { get; set; }
        public string EmailUserInvitation { get; set; }
        public string FirstNameUserInvitation { get; set; }
        public string LastNameUserInvitation { get; set; }
        public string ImageUserInvitation { get; set; }
        public string DisplayNameUserInvitation { get; set; }
        public string IdUserDestinationInvitation { get; set; }
        public string EmailUserDestination { get; set; }
        public string FirstNameUserDestination { get; set; }
        public string LastNameUserDestination { get; set; }
        public string ImageUserDestination { get; set; }
        public string DisplayNameUserDestination { get; set; }
        public DateTime DateSend { get; set; }
        public DateTime DateResponse { get; set; }
        public FriendRequestResponseEnum Response { get; set; }

        public FriendRequest(UserProfile profileSendInvitation, UserProfile profileReceiveInvitation)
        {
            IdUserSendInvitation = profileSendInvitation.Id;
            EmailUserInvitation = profileSendInvitation.Email;
            FirstNameUserInvitation = profileSendInvitation.FirstName;
            LastNameUserInvitation = profileSendInvitation.LastName;
            ImageUserInvitation = profileSendInvitation.Image;
            DisplayNameUserInvitation = profileSendInvitation.DisplayName;
            IdUserDestinationInvitation = profileReceiveInvitation.Id;
            EmailUserDestination = profileReceiveInvitation.Email;
            FirstNameUserDestination = profileReceiveInvitation.FirstName;
            LastNameUserDestination = profileReceiveInvitation.LastName;
            ImageUserDestination = profileReceiveInvitation.Image;
            this.DateSend = DateTime.Now;
            this.Response = FriendRequestResponseEnum.Sended;
        }

        public FriendRequest()
        {
            
        }

		        public ICommand AcceptInvitationCommand
		{
			get
			{
				return new Command(() =>
			   {
				   var a = "";
				   var adas = a;
				   a = adas;

			   });
			}
		}
    }
}
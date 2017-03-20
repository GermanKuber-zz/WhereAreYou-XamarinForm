using System;
using System.Threading.Tasks;
using WhereAreYouMobile.ViewModels.Friends.UserControls;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereAreYouMobile.Views.Friends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : TabbedPage
    {
        public FriendsPage()
        {
            InitializeComponent();

        }

        private async Task FriendsPage_OnAppearing(object sender, EventArgs e)
        {
            await ((InvitationSendedUserControlViewModel)this.searchFriendUserControl.BindingContext).OnAppearingAsync();
        }
    }
}

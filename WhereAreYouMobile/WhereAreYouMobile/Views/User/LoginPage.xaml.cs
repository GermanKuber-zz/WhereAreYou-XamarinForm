using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereAreYouMobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }


        private async Task Login_OnAppearing(object sender, EventArgs e)
        {
          
            await ((LoginViewModel)this.BindingContext).OnAppearingAsync();

        }

    }
}

using WhereAreYouMobile.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereAreYouMobile.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();
        }
    }
}

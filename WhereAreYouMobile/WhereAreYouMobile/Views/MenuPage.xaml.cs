using System;
using WhereAreYouMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereAreYouMobile.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
         public MenuPage()
        {
            InitializeComponent();

        }
    }
    public enum MenuType
    {
        About,
        Blog,
        Twitter,
        Hanselminutes,
        Ratchet,
        DeveloperLife
    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem(Action navigatedCallback)
        {
            this.NavigatedCallback = navigatedCallback;
        }
        public Action NavigatedCallback { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
    }
    public class BaseModel
    {
        public BaseModel()
        {
        }
        public string Id { get; set; }

    }
}
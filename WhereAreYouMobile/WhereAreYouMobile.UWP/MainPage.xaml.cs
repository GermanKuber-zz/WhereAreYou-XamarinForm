using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WhereAreYouMobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("qBfJeaHd0ewFeCkKqclA~S5OxSoHdwP0z_8KFgzYt9w~Ald9JtxgnsimzEswi8M4LBiTxzV3mKokR5Onx7FB-M4zeDZlLp15B_vgO0vopaty");
            LoadApplication(new WhereAreYouMobile.App());
        }
    }
}

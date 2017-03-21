using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WhereAreYouMobile.UserControls
{
    [ContentProperty("ContainerContent")]
    public partial class LoadingUserControl : ContentView, INotifyPropertyChanged
    {

        public LoadingUserControl()
        {
            InitializeComponent();
        }

        #region Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public View ContainerContent
        {
            get { return ContentFrame.Content; }
            set { ContentFrame.Content = value; }
        }
        public Color ContentBackgroundColor
        {
            get { return ContentFrame.BackgroundColor; }
            set { ContentFrame.BackgroundColor = value; }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
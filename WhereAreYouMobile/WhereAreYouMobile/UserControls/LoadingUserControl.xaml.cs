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


        public static readonly BindableProperty InputProperty = BindableProperty.Create(nameof(InputView),
                                                                                        typeof(bool),
                                                                                        typeof(LoadingUserControl),
                                                                                        false,
                                                                                        BindingMode.Default,
                                                                                        null,
                                                                                        InputPropertyChanged);



        private static void InputPropertyChanged(BindableObject bindable, object oldValue, object newValue)

        {

            var self = (LoadingUserControl)bindable;

            self.IsBusy = (bool)newValue;

        }
    }
}
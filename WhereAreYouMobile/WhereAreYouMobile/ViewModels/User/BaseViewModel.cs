using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services;
using WhereAreYouMobile.Views;
using Xamarin.Forms;

namespace WhereAreYouMobile.ViewModels.User
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            LoggerService = DependencyService.Get<ILoggerService>();
        }



        #region Properties

        public ILoggerService LoggerService { get; set; }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string icon = null;
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Protected Methods

        
        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            return true;
        }

        protected async Task CallWithLoadingAsync(CallWithLoadingDelegate callAction)
        {
            if (callAction ==null)
                throw  new ArgumentNullException(nameof(callAction));

            try
            {
                IsBusy = true;
                callAction.Invoke();
                IsBusy = false;
            }
            catch (Exception e)
            {
                await LoggerService.LogErrorAsync(e);
                throw;
            }
         

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
        public virtual async Task OnAppearingAsync()
        {

        }
        public delegate void CallWithLoadingDelegate();
    }

}
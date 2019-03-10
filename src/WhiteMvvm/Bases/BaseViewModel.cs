using System.Threading.Tasks;
using WhiteMvvm.Configuration;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Bases
{
    public class BaseViewModel : NotifiedObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        bool _isInitialize;
        private bool _isBusy;
        public object NavigationData { get; private set; }
        public BaseViewModel()
        {
            DialogService = BaseLocator.Instance.Resolve<IDialogService>();
            NavigationService = BaseLocator.Instance.Resolve<INavigationService>();
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (ConfigurationManager.Current.UseBaseIndicator)
                {
                    if (value)
                    {

                        DialogService.ShowLoading(ConfigurationManager.Current.IndicatorMaskType);
                    }
                    else
                    {
                        DialogService.HideLoading();
                    } 
                }
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        protected internal virtual Task OnPopupAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnPopupDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual void Initialize(object navigationData)
        {
            NavigationData = navigationData;
        }
        internal void InternalInitialize(object navigationData)
        {
            if (_isInitialize)
                return;
            _isInitialize = true;
            Initialize(navigationData);
        }
        protected internal virtual bool HandleBackButton()
        {
            return true;
        }
    }
}

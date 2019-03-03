using System.Threading.Tasks;
using Acr.UserDialogs;
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
            DialogService = BaseViewModelLocator.Resolve<IDialogService>();
            NavigationService = BaseViewModelLocator.Resolve<INavigationService>();
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (ViewModelConfig.Current.UseBaseIndicator)
                {
                    if (value)
                    {

                        DialogService.ShowLoading(ViewModelConfig.Current.IndicatorMaskType);
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
        protected internal virtual Task InitializeAsync(object navigationData)
        {
            NavigationData = navigationData;
            return Task.CompletedTask;
        }
        internal Task InternalInitializeAsync(object navigationData)
        {
            if (_isInitialize)
                return Task.CompletedTask;
            _isInitialize = true;
            return InitializeAsync(navigationData);
        }
        protected internal virtual bool HandleBackButton()
        {
            return true;
        }
    }
}

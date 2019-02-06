using System.Threading.Tasks;
using Acr.UserDialogs;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Bases
{
    public class BaseViewModel : NotifiedObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        bool _isInialize;
        private bool _isBusy;

        public object NavigationData { get; set; }
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
                if (value)
                {
                    
                    DialogService.ShowLoading(MaskType.Gradient);                    
                }
                else
                {
                       DialogService.HideLoading();
                }
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        protected internal virtual Task OnPopupAppearing()
        {
            return Task.FromResult(false);
        }
        protected internal virtual Task OnPopupDisappearing()
        {
            return Task.FromResult(false);
        }
        protected internal virtual Task OnAppearing()
        {
            return Task.FromResult(false);
        }
        protected internal virtual Task OnDisappearing()
        {
            return Task.FromResult(false);
        }
        protected internal virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        internal Task InternalInitializeAsync(object navigationData)
        {
            if (_isInialize)
                return Task.CompletedTask;
            _isInialize = true;
            return InitializeAsync(navigationData);
        }
        protected internal virtual bool HandleBackButton()
        {
            return true;
        }
    }
}

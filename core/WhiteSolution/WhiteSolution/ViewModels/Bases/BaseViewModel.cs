using System.Threading.Tasks;
using WhiteSolution.Services.Dialog;
using WhiteSolution.Services.Navigation;
using WhiteSolution.Utils;

namespace WhiteSolution.ViewModels.Bases
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
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (value)
                {
                    
                    DialogService.ShowLoading();                    
                }
                else
                {
                       DialogService.HideLoading();
                }
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        internal virtual Task OnPopupAppearing()
        {
            return Task.FromResult(false);
        }
        internal virtual Task OnPopupDisappearing()
        {
            return Task.FromResult(false);
        }
        internal virtual Task OnAppearing()
        {
            return Task.FromResult(false);
        }
        internal virtual Task OnDisappearing()
        {
            return Task.FromResult(false);
        }
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        internal Task InternalInitializeAsync(object navigationData)
        {
            if (!_isInialize)
            {
                _isInialize = true;
                return InitializeAsync(navigationData);
            }
            return Task.CompletedTask;
        }
        public virtual bool HandleBackButton()
        {
            return true;
        }
    }
}

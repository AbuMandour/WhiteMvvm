using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteSolution.Services.Navigation;
using WhiteSolution.Utils;
using WhiteSolution.ViewModels.Bases;

namespace WhiteSolution.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private ObservableRangeCollection<PageContainer> _pages;

        public override Task InitializeAsync(object navigationData)
        {            
            if(navigationData is ObservableRangeCollection<PageContainer> pages)            
                Pages = pages;                            
            else            
                Pages = new ObservableRangeCollection<PageContainer>();
            
            ChangeDetailCommand = new TaskCommand(OnChangeDetail);
            return base.InitializeAsync(navigationData);
        }

        private async Task OnChangeDetail(object arg)
        {
            if(arg is PageContainer pageContainer)
            {
                await NavigationService.ChangeDetailPage(pageContainer);
            }                
        }

        public ObservableRangeCollection<PageContainer> Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged();
            }
        }
        public ICommand ChangeDetailCommand { get; set; }

    }
}

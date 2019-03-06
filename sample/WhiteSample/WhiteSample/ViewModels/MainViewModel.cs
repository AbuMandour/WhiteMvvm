using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;
using Xamarin.Forms;

namespace WhiteSample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand BackCommand { get; set; }
        public MainViewModel()
        {
            BackCommand = new TaskCommand(OnBack);
            //BackCommand = new Command(OnBack);
        }

        private async Task OnBack(object arg)
        {
            await NavigationService.Navigation.PopAsync();
        }
    }
}

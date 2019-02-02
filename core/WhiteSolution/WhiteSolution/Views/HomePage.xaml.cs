using WhiteSolution.Views.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhiteSolution.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BaseContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
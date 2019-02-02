using WhiteSolution.Views.Bases;
using Xamarin.Forms.Xaml;

namespace WhiteSolution.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : BaseContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }
    }
}
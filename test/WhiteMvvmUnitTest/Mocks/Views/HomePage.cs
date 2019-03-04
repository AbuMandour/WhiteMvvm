using WhiteMvvm.Bases;

namespace WhiteMvvmUnitTest.Mocks.Views
{
    public class HomePage : BaseContentPage
    {
        public HomePage()
        {
            SetAutoWireViewModel(this,true);
        }
    }
}

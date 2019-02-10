using WhiteMvvm.Bases;

namespace WhiteMvvmUnitTest.Mocks.Views
{
    public class HomePage : BaseContentPage
    {
        public HomePage()
        {
            BaseViewModelLocator.SetAutoWireViewModel(this,true);
        }
    }
}

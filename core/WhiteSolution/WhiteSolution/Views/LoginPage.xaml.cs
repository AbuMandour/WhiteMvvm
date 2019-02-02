using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.Behaviors;
using WhiteSolution.ViewModels.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhiteSolution.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region Fields
        private Label _lblTitle;
        private Entry _txtUserName;
        private Entry _txtPassword;
        private Button _btnSignIn;
        private Label _lblLangauge;
        private Switch _swLangauge;
        #endregion
        public LoginPage()
        {
            ViewModelLocator.SetAutoWireViewModel(this, true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RendererDesign();
        }

        public void RendererDesign()
        {
            try
            {
                _lblTitle = new Label
                {
                    FontSize = 20,
                    TextColor = Color.Blue,
                };
                _lblTitle.SetBinding(Label.TextProperty, "Title");
                FlexLayout.SetAlignSelf(_lblTitle, FlexAlignSelf.Center);

                _txtUserName = new Entry
                {
                    AutomationId = "userNameEntry",
                };
                _txtUserName.SetBinding(Entry.PlaceholderProperty, "UserNamePlaceHolder");
                _txtUserName.SetBinding(Entry.TextProperty, "UserName", BindingMode.TwoWay);

                _txtPassword = new Entry
                {
                    AutomationId = "passwordEntry",
                };
                _txtPassword.SetBinding(Entry.PlaceholderProperty, "PasswordPlaceHolder");
                _txtPassword.SetBinding(Entry.TextProperty, "Password", BindingMode.TwoWay);
                _txtPassword.IsPassword = true;

                _btnSignIn = new Button
                {
                    AutomationId = "signInButton",
                };
                _btnSignIn.SetBinding(Button.TextProperty, "SignInText");
                _btnSignIn.SetBinding(Button.CommandProperty, "SignInCommand");

                _lblLangauge = new Label();
                _lblLangauge.SetBinding(Label.TextProperty, "Langauge");

                _swLangauge = new Switch
                {
                    AutomationId = "langaugeSwitch",
                };
                FlexLayout.SetAlignSelf(_swLangauge, FlexAlignSelf.Start);
                var swBehavior = new EventToCommandBehavior { EventName = "Toggled" };
                swBehavior.SetBinding(EventToCommandBehavior.CommandProperty, "SwitchLangaugeCommand");
                swBehavior.SetBinding(EventToCommandBehavior.CommandParameterProperty, new Binding(".", source: _swLangauge));
                _swLangauge.Behaviors.Add(swBehavior);

                var flxMainLayout = new FlexLayout
                {
                    Children =
                    {
                        _lblTitle,
                        _txtUserName,
                        _txtPassword,
                        _btnSignIn,
                        _lblLangauge,
                        _swLangauge
                    },
                    Direction = FlexDirection.Column,
                    AlignItems = FlexAlignItems.Stretch,
                    JustifyContent = FlexJustify.SpaceAround
                };
                this.Padding = 10;
                Content = flxMainLayout;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
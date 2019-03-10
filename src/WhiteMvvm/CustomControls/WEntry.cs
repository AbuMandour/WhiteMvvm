using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Validations;
using Xamarin.Forms;

namespace WhiteMvvm.CustomControls
{
    public class WEntry : Entry
    {
        public static readonly BindableProperty ValidationMessageProperty =
            BindableProperty.Create("ValidationMessage", typeof(string),
                typeof(WEntry), string.Empty, BindingMode.TwoWay);

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create("IValid", typeof(bool),
                typeof(WEntry), true, BindingMode.TwoWay);

        public static readonly BindableProperty ValidationListProperty =
            BindableProperty.Create("ValidationList", typeof(IList<IValidatedRule>),
                typeof(WEntry), default(IList<IValidatedRule>), BindingMode.TwoWay);

        public string ValidationMessage
        {
            get { return (string)GetValue(ValidationMessageProperty); }
            set { SetValue(ValidationMessageProperty, value); }
        }
        public IList<IValidatedRule> ValidationList
        {
            get { return (IList<IValidatedRule>)GetValue(ValidationListProperty); }
            set { SetValue(ValidationListProperty, value); }
        }

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public WEntry()
        {
            this.TextChanged += WEntry_TextChanged;
        }

        private void WEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0)
            {
                foreach (var validity in ValidationList)
                {
                    validity.Validate(e.NewTextValue);
                    ValidationMessage = validity.ValidationMessage;
                    IsValid = validity.IsValid;
                } 
            }
        }
    }
}

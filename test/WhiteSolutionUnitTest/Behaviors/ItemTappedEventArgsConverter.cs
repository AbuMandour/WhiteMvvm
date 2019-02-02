using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WhiteSolutionUnitTest.Behaviors
{
    internal class ItemTappedEventArgsConverter : IValueConverter
    {
        private readonly bool _returnParameter;

        public bool HasConverted { get; private set; }

        public ItemTappedEventArgsConverter(bool returnParameter)
        {
            _returnParameter = returnParameter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HasConverted = true;
            return _returnParameter ? parameter : (value as ItemTappedEventArgs)?.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

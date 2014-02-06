using System;
using System.Windows.Data;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    public class RutaVideoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                return AppDomain.CurrentDomain.BaseDirectory + "videos\\" + value;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

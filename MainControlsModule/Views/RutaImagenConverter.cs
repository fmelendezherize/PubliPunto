
using System;
using System.Windows.Data;
namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    public class RutaImagenConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string obj = value as string;
            if (obj != null)
            {
                return AppDomain.CurrentDomain.BaseDirectory + "media\\" + obj;
            }
            return AppDomain.CurrentDomain.BaseDirectory + "media\\logo.png";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}

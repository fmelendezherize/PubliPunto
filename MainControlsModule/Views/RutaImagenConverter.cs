
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System;
using System.Windows.Data;
namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    public class RutaImagenConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnteComercial obj = value as EnteComercial;
            if (obj != null)
            {
                return AppDomain.CurrentDomain.BaseDirectory + "media\\" + obj.ImagenUrl;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}

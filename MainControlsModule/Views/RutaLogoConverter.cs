using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System;
using System.Windows.Data;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    public class RutaLogoConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnteComercial obj = value as EnteComercial;
            if (obj != null)
            {
                return AppDomain.CurrentDomain.BaseDirectory + "Data\\" + obj.Id + "\\" + obj.LogoUrl;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}

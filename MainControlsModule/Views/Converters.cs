
using System;
using System.Windows.Data;
namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    public class RutaImagenConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string obj = value as string;
            if (!String.IsNullOrEmpty(obj))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "media\\" + obj;
                if (System.IO.File.Exists(path)) return path;
            }
            return AppDomain.CurrentDomain.BaseDirectory + "media\\logo.png";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

    public class RutaVideoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "videos\\" + value;
                if (System.IO.File.Exists(path)) return path;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FechaConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.DateTime? dt = value as System.DateTime?;
            if (dt == null) { return string.Empty; }
            return dt.Value.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

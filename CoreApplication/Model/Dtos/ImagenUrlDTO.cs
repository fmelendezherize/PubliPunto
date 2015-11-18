
using System;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class ImagenUrlDTO
    {
        public ImageDTO image { get; set; }
    }

    public class ImagenBannerUrlDTO
    {
        public ImageDTO banner { get; set; }
    }

    public class ImageDTO
    {
        public string url { get; set; }

        public string ToFileName()
        {
            if (url == null) return string.Empty;
            Uri webpath = new Uri("file://localhost" + url);
            if (webpath.IsFile)
            {
                return System.IO.Path.GetFileName(webpath.LocalPath);
            }
            return null;
        }
    }

    public class LogoUrlDTO
    {
        public LogoDTO logo { get; set; }
    }

    public class LogoDTO
    {
        public string url { get; set; }

        public string ToFileName()
        {
            if (url == null) return string.Empty;
            Uri webpath = new Uri("file://localhost" + url);
            if (webpath.IsFile)
            {
                return System.IO.Path.GetFileName(webpath.LocalPath);
            }
            return null;
        }
    }
}

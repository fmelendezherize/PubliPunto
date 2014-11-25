
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class ImagenUrlDTO
    {
        public ImageDTO image { get; set; }
    }

    public class ImageDTO
    {
        public string url { get; set; }
    }

    public class LogoUrlDTO
    {
        public LogoDTO logo { get; set; }
    }

    public class LogoDTO
    {
        public string url { get; set; }
    }
}

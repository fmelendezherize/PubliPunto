using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace Decktra.PubliPuntoEstacion
{
    class Logger : ILoggerFacade
    {
        private LogWriter logWriter;

        public Logger()
        {
            LogWriterFactory logWriterFactory = new LogWriterFactory();
            logWriter = logWriterFactory.Create();
        }

        public void Log(string message, Category category, Priority priority)
        {
            if (category == Category.Info) logWriter.Write(message, "FileOutput");
        }
    }
}


using Decktra.PubliPuntoEstacion.Library;
using System;
namespace Decktra.PubliPuntoEstacion.KioskoServicesModule
{
    public class GotoHomeTimerService
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private int _counter;
        private int _timerInactividad;

        public GotoHomeTimerService(int timerInactividad)
        {
            this._timerInactividad = timerInactividad;
        }

        public void Start()
        {
            if (dispatcherTimer != null) { dispatcherTimer.Stop(); }

            _counter = _timerInactividad;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = sender as System.Windows.Threading.DispatcherTimer;
            _counter -= 1;
            if (_counter == 0)
            {
                GlobalCommands.GoToHomeCommand.Execute(null);
            }
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
            dispatcherTimer = null;
        }
    }
}

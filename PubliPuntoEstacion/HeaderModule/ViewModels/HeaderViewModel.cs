using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Decktra.PubliPuntoEstacion.HeaderModule.ViewModels
{
    class HeaderViewModel : INotifyPropertyChanged
    {
        public HeaderViewModel()
        {
            Debug.Print("Iniciando HeaderViewModel");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}

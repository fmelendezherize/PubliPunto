using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Decktra.PubliPuntoEstacion.HeaderModule.ViewModels
{
    class HeaderViewModel : INotifyPropertyChanged
    {
        public HeaderViewModel() 
        {
            Debug.Print("Iniciando");
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

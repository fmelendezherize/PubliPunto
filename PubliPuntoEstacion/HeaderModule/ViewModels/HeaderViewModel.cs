using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Decktra.PubliPuntoEstacion.HeaderModule.ViewModels
{
    class HeaderViewModel : INotifyPropertyChanged
    {
        public ICommand GoToHomeCommand { get; set; }

        public HeaderViewModel() 
        {
            Debug.Print("Iniciando");
           
            this.GoToHomeCommand = new DelegateCommand<Object>(this.GoToHome);
        }

        private void GoToHome(object obj)
        {
 	        Debug.Print("Llendo a Home");
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

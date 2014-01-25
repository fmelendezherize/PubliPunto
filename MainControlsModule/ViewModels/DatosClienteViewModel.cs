using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class DatosClienteViewModel : NotificationObject
    {
        private readonly EnteComercialRepository _enteComercialRepository;
        public ICommand ShowEnteComercialCommand;
        public EnteComercial EnteComercial { get; set; }
        public string RutaImagen { get; set; }


        public DatosClienteViewModel()
        {
            ShowEnteComercialCommand = new DelegateCommand<string>(this.ShowEnteComercial);
            _enteComercialRepository = new EnteComercialRepository();
        }

        private void ShowEnteComercial(string obj)
        {
            EnteComercial = _enteComercialRepository.FindBy(int.Parse(obj));
            if (EnteComercial == null) GlobalCommands.GoToHomeCommand.Execute(null);

            this.RutaImagen = AppDomain.CurrentDomain.BaseDirectory + "Data\\" + EnteComercial.Id + "\\" + EnteComercial.ImagenUrl;
            this.RaisePropertyChanged(() => this.RutaImagen);

            this.RaisePropertyChanged(() => this.EnteComercial);
        }
    }
}
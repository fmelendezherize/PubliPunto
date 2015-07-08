using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class OfertasViewModel : NotificationObject
    {
        private readonly EnteComercialRepository _enteComercialRepository;
        private Random _randomObj;

        public ICommand ShowEnteComercialsCommand { get; set; }
        public EnteComercial EnteComercial0 { get; set; }
        public EnteComercial EnteComercial1 { get; set; }
        public EnteComercial EnteComercial2 { get; set; }

        public OfertasViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this._randomObj = new Random(DateTime.Now.Millisecond + 100);
            this.ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
        }

        private void ShowEnteComercials(object obj)
        {
            //Obtengo promociones activas de distintos entescomerciales
            var result = (from q in _enteComercialRepository.GetPromocionesActivas().GroupBy(q => q.EnteComercialId)
                          select (q.FirstOrDefault())).ToList();

            var listEnteComercials = (from q in result
                                      select new
                                      {
                                          Id = q.EnteComercialId
                                      }).ToList();

            if (listEnteComercials.Count > 3)
            {
                //Random Position
                int?[] indexEnteComercials = new int?[3];
                for (int i = 0; i <= 2; i++)
                {
                    //indexEnteComercials[i] = GetIndex(indexEnteComercials, listEnteComercials);
                    //Eliminando Random
                    indexEnteComercials[i] = i;
                }
                EnteComercial0 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[0].Value].Id);
                EnteComercial1 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[1].Value].Id);
                EnteComercial2 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[2].Value].Id);
            }
            else
            {
                if (listEnteComercials.ElementAtOrDefault(0) == null)
                {
                    EnteComercial0 = new EnteComercial()
                    {
                        Id = 0,
                        Nombre = "Disponible",
                        Direccion = "Direccion de su negocio o empresa.",
                        Telefonos = "Telefonos para que sus clientes lo contacten.",
                        WebAddress = "www.tudireccionweb.com"
                    };
                }
                else
                {
                    EnteComercial0 = _enteComercialRepository.FindBy(listEnteComercials[0].Id);
                }
                if (listEnteComercials.ElementAtOrDefault(1) == null)
                {
                    EnteComercial1 = new EnteComercial()
                    {
                        Id = 0,
                        Nombre = "Disponible",
                        Direccion = "Direccion de su negocio o empresa.",
                        Telefonos = "Telefonos para que sus clientes lo contacten.",
                        WebAddress = "www.tudireccionweb.com"
                    };
                }
                else
                {
                    EnteComercial1 = _enteComercialRepository.FindBy(listEnteComercials[1].Id);
                }
                if (listEnteComercials.ElementAtOrDefault(2) == null)
                {
                    EnteComercial2 = new EnteComercial()
                    {
                        Id = 0,
                        Nombre = "Disponible",
                        Direccion = "Direccion de su negocio o empresa.",
                        Telefonos = "Telefonos para que sus clientes lo contacten.",
                        WebAddress = "www.tudireccionweb.com"
                    };
                }
                else
                {
                    EnteComercial2 = _enteComercialRepository.FindBy(listEnteComercials[2].Id);
                }
            }
            this.RaisePropertyChanged(() => this.EnteComercial0);
            this.RaisePropertyChanged(() => this.EnteComercial1);
            this.RaisePropertyChanged(() => this.EnteComercial2);
        }

        private int GetIndex(int?[] indexEnteComercials, IList listEnteComercials)
        {
            //Thread.Sleep(100);
            int intRamdon = this._randomObj.Next(0, listEnteComercials.Count);
            //TODO Buscar solucion para el stackoverflow aqui
            if (indexEnteComercials.Any(q => q == intRamdon))
            {
                return GetIndex(indexEnteComercials, listEnteComercials);
            }
            return intRamdon;
        }
    }
}
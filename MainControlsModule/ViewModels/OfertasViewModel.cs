using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class OfertasViewModel : NotificationObject
    {
        private readonly EnteComercialRepository _enteComercialRepository;
        private Random _randomObj;

        public ICommand ShowEnteComercialsCommand { get; set; }
        public Promocion EnteComercial0 { get; set; }
        public Promocion EnteComercial1 { get; set; }
        public Promocion EnteComercial2 { get; set; }

        public OfertasViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this._randomObj = new Random(DateTime.Now.Millisecond + 100);
            this.ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
        }

        private void ShowEnteComercials(object obj)
        {
            var result = (from q in _enteComercialRepository.GetPromocionesVigentes() select q).ToList();

            EnteComercial0 = result.ElementAtOrDefault<Promocion>(0) ?? new Promocion() { EnteComercial = new EnteComercial() { Nombre = "Disponible" } };
            EnteComercial1 = result.ElementAtOrDefault<Promocion>(1) ?? new Promocion() { EnteComercial = new EnteComercial() { Nombre = "Disponible" } };
            EnteComercial2 = result.ElementAtOrDefault<Promocion>(2) ?? new Promocion() { EnteComercial = new EnteComercial() { Nombre = "Disponible" } };

            this.RaisePropertyChanged(() => this.EnteComercial0);
            this.RaisePropertyChanged(() => this.EnteComercial1);
            this.RaisePropertyChanged(() => this.EnteComercial2);
        }

        //private int GetIndex(int?[] indexEnteComercials, IList listEnteComercials)
        //{
        //    //Thread.Sleep(100);
        //    int intRamdon = this._randomObj.Next(0, listEnteComercials.Count);
        //    //TODO Buscar solucion para el stackoverflow aqui
        //    if (indexEnteComercials.Any(q => q == intRamdon))
        //    {
        //        return GetIndex(indexEnteComercials, listEnteComercials);
        //    }
        //    return intRamdon;
        //}
    }
}
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class NuestrosClientesViewModel : NotificationObject
    {
        private readonly EnteComercialRepository _enteComercialRepository;
        public ICommand ShowEnteComercialsCommand { get; set; }

        private const short CANT_CLIENTES_VISIBLE = 4;

        public List<Promocion> ListOfPromocionesActivas { get; set; }
        private Random _randomObj;

        public NuestrosClientesViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this._randomObj = new Random(DateTime.Now.Millisecond);
            this.ListOfPromocionesActivas = new List<Promocion>();
            this.ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
        }

        private void ShowEnteComercials(object obj)
        {
            var listOfResult = _enteComercialRepository.GetPromocionesVigentes().ToList();
            this.ListOfPromocionesActivas.Clear();

            if (listOfResult.Count > CANT_CLIENTES_VISIBLE)
            {
                ShowRandomEnteComercials(listOfResult);
                return;
            }

            for (int i = listOfResult.Count; i < CANT_CLIENTES_VISIBLE; i++)
            {
                listOfResult.Add(new Promocion
                {
                    Id = 0,
                    EnteComercial = new EnteComercial() { Nombre = "Disponible" },
                });
            }

            this.ListOfPromocionesActivas.AddRange(listOfResult);
            RaisePropertyChanged(() => this.ListOfPromocionesActivas);
        }

        private void ShowRandomEnteComercials(IList listOfResults)
        {
            int?[] indexEnteComercials = new int?[CANT_CLIENTES_VISIBLE];

            for (int i = 0; i < CANT_CLIENTES_VISIBLE; i++)
            {
                indexEnteComercials[i] = GetIndex(indexEnteComercials, listOfResults);
                ListOfPromocionesActivas.Add((Promocion)listOfResults[indexEnteComercials[i].Value]);
            }
            RaisePropertyChanged(() => this.ListOfPromocionesActivas);
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

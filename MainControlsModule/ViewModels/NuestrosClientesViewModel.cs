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

        public List<EnteComercial> ListOfEnteComercials { get; set; }
        private Random _randomObj;

        public NuestrosClientesViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this._randomObj = new Random(DateTime.Now.Millisecond);
            this.ListOfEnteComercials = new List<EnteComercial>();
            this.ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
        }

        private void ShowEnteComercials(object obj)
        {
            var listOfResult = (from q in _enteComercialRepository.GetAll() where (q.IsActivo) select q).ToList();
            this.ListOfEnteComercials.Clear();

            if (listOfResult.Count > 12)
            {
                ShowRandomEnteComercials(listOfResult);
                return;
            }

            for (int i = listOfResult.Count; i < 12; i++)
            {
                listOfResult.Add(new EnteComercial
                {
                    Id = 0,
                    Nombre = "Disponible",
                    Direccion = "Direccion de su negocio o empresa.",
                    Telefonos = "Telefonos para que sus clientes lo contacten.",
                    WebAddress = "www.tudireccionweb.com"
                });
            }

            this.ListOfEnteComercials.AddRange(listOfResult);
            RaisePropertyChanged(() => this.ListOfEnteComercials);
        }

        private void ShowRandomEnteComercials(IList listOfResults)
        {
            int?[] indexEnteComercials = new int?[12];


            for (int i = 0; i < 12; i++)
            {
                indexEnteComercials[i] = GetIndex(indexEnteComercials, listOfResults);
                ListOfEnteComercials.Add((EnteComercial)listOfResults[indexEnteComercials[i].Value]);
            }
            RaisePropertyChanged(() => this.ListOfEnteComercials);
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

﻿using Decktra.PubliPuntoEstacion.CoreApplication.Model;
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
        public ICommand ShowEnteComercialsCommand { get; set; }

        public EnteComercial EnteComercial0 { get; set; }
        public EnteComercial EnteComercial1 { get; set; }
        public EnteComercial EnteComercial2 { get; set; }

        public OfertasViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this.ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
        }

        private void ShowEnteComercials(object obj)
        {
            var listEnteComercials = (from q in _enteComercialRepository.GetAll()
                                      select new
                                      {
                                          Id = q.Id
                                      }).ToList();

            int?[] indexEnteComercials = new int?[3];
            for (int i = 0; i <= 2; i++)
            {
                indexEnteComercials[i] = GetIndex(indexEnteComercials, listEnteComercials);
            }

            EnteComercial0 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[0].Value].Id);
            EnteComercial1 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[1].Value].Id);
            EnteComercial2 = _enteComercialRepository.FindBy(listEnteComercials[indexEnteComercials[2].Value].Id);
            this.RaisePropertyChanged(() => this.EnteComercial0);
            this.RaisePropertyChanged(() => this.EnteComercial1);
            this.RaisePropertyChanged(() => this.EnteComercial2);
        }

        private int GetIndex(int?[] indexEnteComercials, IList listEnteComercials)
        {
            int intRamdon = new Random(DateTime.Now.Millisecond).Next(0, listEnteComercials.Count);
            //TODO Buscar solucion para el stackoverflow aqui
            if (indexEnteComercials.Any(q => q == intRamdon))
            {
                return GetIndex(indexEnteComercials, listEnteComercials);
            }
            return intRamdon;
        }
    }
}
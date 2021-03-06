﻿using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for NuestrosClientesView.xaml
    /// </summary>
    public partial class NuestrosClientesView : UserControl
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public NuestrosClientesView()
        {
            InitializeComponent();
            ShowClientes();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            Promocion promocionSelected = button.DataContext as Promocion;
            if (promocionSelected != null && promocionSelected.EnteComercialId != 0)
            {
                NavigationParameters query = new NavigationParameters();
                query.Add("ID", promocionSelected.EnteComercial.Id.ToString());
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("DatosClienteView" + query.ToString(), UriKind.Relative));
            }

            //this.WrapPanelClientes.Visibility = System.Windows.Visibility.Collapsed;
            //this.WrapPanelVideo.Visibility = System.Windows.Visibility.Visible;
            //this.Video.Play();
        }

        public void ShowClientes()
        {
            ((NuestrosClientesViewModel)this.DataContext).ShowEnteComercialsCommand.Execute(null);
        }

        public void StopVideo()
        {
            //this.Video.Stop();
        }
    }
}

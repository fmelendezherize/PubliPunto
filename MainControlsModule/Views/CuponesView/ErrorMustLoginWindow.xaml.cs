﻿using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for ErrorMustLoginWindow.xaml
    /// </summary>
    public partial class ErrorMustLoginWindow : Window
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public ErrorMustLoginWindow()
        {
            InitializeComponent();
        }

        private void OnWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        public void OnNavigatedTo(string accion)
        {
            if (accion == "ErrorFormularioLibre")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Debe llenar por completo el formulario para poder reclamar un cupón a traves de este kiosko.";
            }
            else if (accion == "ErrorLogin")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Su cédula de identidad o PIN son incorrectos. Por favor vuelva a introducirlos.";
            }
        }
    }
}
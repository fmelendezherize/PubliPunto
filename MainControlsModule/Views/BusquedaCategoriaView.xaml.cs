using Decktra.PubliPuntoEstacion.Library;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for BusquedaCategoriaView.xaml
    /// </summary>
    public partial class BusquedaCategoriaView : UserControl, INavigationAware
    {
        public BusquedaCategoriaView()
        {
            this.InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //nada
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ((BusquedaCategoriaViewModel)this.DataContext).Init();
        }

        private void CategoryItemControl_OnOnListViewCategoriaMouseClick(object sender, EventArgs e)
        {
            if (IsPressed) return;

            var selectedSubCategoria = sender as SubCategoria;
            if (selectedSubCategoria != null)
            {
                if (selectedSubCategoria.TipoSubCategoria == TipoSubCategoria.RamoComercial)
                {
                    ((BusquedaCategoriaViewModel)this.DataContext).ShowEnteComercialsCommand.Execute(selectedSubCategoria.Id);
                }
                else
                {
                    GlobalCommands.GoToDatosClienteCommand.Execute(selectedSubCategoria.Id);
                }
            }
        }

        private bool IsPressed = false;
        private double InitY;
        private void ListViewCategorias_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsPressed = true;
                const double DBL_Offtset = .6;
                if (e.GetPosition(TextBlockTitulo).Y > InitY)
                {
                    //Bajo
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset + DBL_Offtset);
                    InitY = e.GetPosition(TextBlockTitulo).Y - DBL_Offtset;
                }
                else
                {
                    //Subio
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset - DBL_Offtset);
                    InitY = e.GetPosition(TextBlockTitulo).Y + DBL_Offtset;
                }
            }
        }

        private void ListViewCategorias_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsPressed)
            {
                IsPressed = false;
            }
        }

        private void ListViewCategorias_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InitY = e.GetPosition(TextBlockTitulo).Y;
        }
    }
}
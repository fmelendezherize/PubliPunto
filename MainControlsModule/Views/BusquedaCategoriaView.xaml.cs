using System;
using System.Windows;
using System.Windows.Input;
using Decktra.PubliPuntoEstacion.MainControlsModule.Controls;
using System.Collections.Generic;
using System.Windows.Controls;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Decktra.PubliPuntoEstacion.Library;

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
			((BusquedaCategoriaViewModel) this.DataContext).Init();
		}

		private void CategoryItemControl_OnOnListViewCategoriaMouseClick(object sender, EventArgs e)
		{
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
	}
}
﻿using System.Windows;
using System.Windows.Input;
using Decktra.PubliPuntoEstacion.MainControlsModule.Controls;
using System.Collections.Generic;
using System.Windows.Controls;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Microsoft.Practices.Prism.Regions;

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

	    public void LoadCategorias()
	    {
	        List<Categoria> listCategorias = new List<Categoria>();
	        Categoria aCategoria = new Categoria();
	        aCategoria.NombreCategoria = "A";
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Alimentos"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Anillo"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Antiguedades"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Autos"});
	        listCategorias.Add(aCategoria);

	        aCategoria = new Categoria();
	        aCategoria.NombreCategoria = "B";
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Bancos"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Baños"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Bowling"});
	        listCategorias.Add(aCategoria);

	        aCategoria = new Categoria();
	        aCategoria.NombreCategoria = "C";
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Carros"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Casas"});
	        aCategoria.ListCategorias.Add(new SubCategoria() {Nombre = "Confiteria"});
	        listCategorias.Add(aCategoria);

	        this.ListViewCategorias.ItemsSource = null;
	        this.ListViewCategorias.ItemsSource = listCategorias;
	    }

	    private void OnListViewItemClick(object sender, MouseButtonEventArgs e)
        {
            List<Categoria> listCategorias = new List<Categoria>();
            Categoria aCategoria = new Categoria();
            aCategoria.NombreCategoria = "Alimentos";
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Pizzerias" });
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Heladerias" });
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Comida China" });
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Comida Mexicana" });
            listCategorias.Add(aCategoria);

            this.ListViewCategorias.ItemsSource = listCategorias;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.LoadCategorias();
        }
    }
}
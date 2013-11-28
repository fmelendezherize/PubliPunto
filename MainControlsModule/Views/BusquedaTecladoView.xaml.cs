using System.Windows.Input;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
	/// <summary>
	/// Interaction logic for BusquedaTecladoView.xaml
	/// </summary>
	public partial class BusquedaTecladoView : UserControl
	{
		public BusquedaTecladoView()
		{
			this.InitializeComponent();
            this.touchKeyboard.SetControlToWrite(this.TextBoxSearch);
            this.LoadData();
		}

	    public void LoadData()
	    {
            List<Categoria> listCategorias = new List<Categoria>();
            Categoria aCategoria = new Categoria();
	        aCategoria.NombreCategoria = "Resultado de la búsqueda";
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Zara" });
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Daka" });
            aCategoria.ListCategorias.Add(new SubCategoria() { Nombre = "Adidas" });
            listCategorias.Add(aCategoria);

            this.ListViewCategorias.ItemsSource = null;
            this.ListViewCategorias.ItemsSource = listCategorias;
	    }
	}
}
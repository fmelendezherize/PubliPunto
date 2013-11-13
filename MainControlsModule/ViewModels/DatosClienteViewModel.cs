using System;
using System.ComponentModel;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
	public class DatosClienteViewModel : INotifyPropertyChanged
	{
		public DatosClienteViewModel()
		{
			
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion
	}
}
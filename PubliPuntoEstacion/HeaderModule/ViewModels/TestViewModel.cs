using System;
using System.ComponentModel;

namespace Decktra.PubliPuntoEstacion.HeaderModule.ViewModels
{
	public class TestViewModel : INotifyPropertyChanged
	{
		public TestViewModel()
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
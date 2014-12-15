using System;
using System.Windows;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Controls
{
    /// <summary>
    /// Interaction logic for CategoryItemControl.xaml
    /// </summary>
    public partial class CategoryItemControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(CategoryItemControl));
        public static readonly DependencyProperty HeaderTextStyleProperty =
            DependencyProperty.Register("HeaderTextStyle", typeof(Style), typeof(CategoryItemControl));
        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(CategoryItemControl));

        public CategoryItemControl()
        {
            InitializeComponent();

            this.ListViewCategorias.SelectionChanged += ListViewCategorias_SelectionChanged;
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public Style HeaderTextStyle
        {
            get { return (Style)GetValue(HeaderTextStyleProperty); }
            set { SetValue(HeaderTextStyleProperty, value); }
        }

        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        public event EventHandler OnCategoriaSelected;

        private void ListViewCategorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewCategorias.SelectedItem == null) return;

            if (OnCategoriaSelected == null) return;
            OnCategoriaSelected(this.ListViewCategorias.SelectedItem, e);
        }
    }
}

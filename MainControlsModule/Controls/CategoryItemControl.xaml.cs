using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public event EventHandler OnListViewCategoriaMouseClick;

        private void ListViewCategorias_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnListViewCategoriaMouseClick(this.ListViewCategorias.SelectedItem, e);
        }
    }
}

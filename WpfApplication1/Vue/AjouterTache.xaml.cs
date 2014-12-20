using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TodoListUCBL.WPFView.Vue
{
    /// <summary>
    /// Logique d'interaction pour AjouterTache.xaml
    /// </summary>
    public partial class AjouterTache : Window
    {
        public AjouterTache()
        {
            InitializeComponent();
        }

        public AjouterTache(ModeleVue.AjouterTacheMV atmv) : this()
        {
            this.DataContext = atmv;
        }

        private void ConfirmationAjoutTache(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

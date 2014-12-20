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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoListUCBL.WPFView.ModeleVue;

namespace TodoListUCBL.WPFView.Vue
{
    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : UserControl
    {
        public static readonly DependencyProperty ConnectionInfosProperty
    = DependencyProperty.Register("ConnectionInfos", typeof(ConnectionInfosMV)
    , typeof(Inscription));

        public ConnectionInfosMV ConnectionInfos
        {
            get
            {
                return (ConnectionInfosMV)GetValue(ConnectionInfosProperty);
            }
            set
            {
                SetValue(ConnectionInfosProperty, value);
            }
        }

        public Inscription()
        {
            InitializeComponent();
        }
    }
}

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
using TodoListUCBL.WPFView.Tools;

namespace TodoListUCBL.WPFView.Vue
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Authentification : UserControl
    {
        public static readonly DependencyProperty ConnectionInfosProperty 
            = DependencyProperty.Register("ConnectionInfos", typeof(ConnectionInfosMV)
            , typeof(Authentification));

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


        public Authentification()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(this.ConnectionInfos != null)
                this.ConnectionInfos.Password= this.Password.Password;
        }

    }
}

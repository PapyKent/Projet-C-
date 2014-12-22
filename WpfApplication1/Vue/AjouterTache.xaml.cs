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
using TodoListUCBL.BusinessEntities;

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

        public AjouterTache(ModeleVue.AjouterTacheMV atmv, List<BECategory> list) : this()
        {
            this.DataContext = atmv;
            //List<TodoItemCat> items = new List<TodoItemCat>();
            //foreach (BECategory c in list)
            //{
            //    if(c.ParDefaut==true)
            //    {
            //        items.Add(new TodoItemCat() { Nom = c.Nom, Pardefaut = c.ParDefaut, Id = c.Id, User = c.Utilisateur.Id });
            //    }               
            //}
            //foreach (BECategory c in list)
            //{
            //    if (c.ParDefaut != true)
            //    {
            //        items.Add(new TodoItemCat() { Nom = c.Nom, Pardefaut = c.ParDefaut, Id = c.Id, User = c.Utilisateur.Id });
            //    }
            //}
            this.ListCategory.ItemsSource = list;
        }

        private void ConfirmationAjoutTache(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
    public class SelectableObject<T>
    {
        public bool IsSelected { get; set; }
        public T ObjectData { get; set; }

        public SelectableObject(T objectData)
        {
            ObjectData = objectData;
        }

        public SelectableObject(T objectData, bool isSelected)
        {
            IsSelected = isSelected;
            ObjectData = objectData;
        }
    }
}

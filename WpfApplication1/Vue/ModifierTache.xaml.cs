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
    /// Logique d'interaction pour ModifierTache.xaml
    /// </summary>
    public partial class ModifierTache : Window
    {
        private List<BECategory> OriginalCategories;
        private List<BECategory> CategoriesToAdd;

        public List<BECategory> CategoriesToAdd1
        {
            get { return CategoriesToAdd; }
            set { CategoriesToAdd = value; }
        }
        public ModifierTache()
        {
            InitializeComponent();
        }

        public ModifierTache(ModeleVue.ModifierTacheMV mtmv, List<BECategory> listAll, List<BECategory> listUsed,  List<BEEtat> etats)
            : this()
        {
            this.DataContext = mtmv;
            List<BECategory> tmp = new List<BECategory>();
            foreach(BECategory c in listAll)
            {
                foreach(BECategory cat in listUsed)
                {
                    if(c.Id==cat.Id)
                    {
                        tmp.Add(c);
                    }
                }
            }

            foreach(BECategory c in tmp)
            {
                listAll.Remove(c);
            }

            OriginalCategories = listAll;
            CategoriesToAdd = listUsed;
            this.ListEtat.ItemsSource = etats;
            int i = 0;
            foreach(BEEtat e in etats)
            {
                if(e.Id == mtmv.Etat.Id)
                {
                    this.ListEtat.SelectedIndex = i;
                }
                i++;
            }
            this.Refresh();
        }

        private void ModifierTacheButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddCateg_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListEntr.SelectedItem != null)
            {
                BECategory c = this.ListEntr.SelectedItem as BECategory;
                this.CategoriesToAdd.Add(c);
                OriginalCategories.Remove(c);
                this.Refresh();
            }
        }

        private void RemoveCateg_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListSort.SelectedItem != null)
            {
                BECategory c = this.ListSort.SelectedItem as BECategory;
                this.OriginalCategories.Add(c);
                CategoriesToAdd.Remove(c);
                this.Refresh();
            }
        }

        private void Refresh()
        {
            ListEntr.ItemsSource = null;
            ListEntr.ItemsSource = OriginalCategories;
            ListSort.ItemsSource = null;
            ListSort.ItemsSource = CategoriesToAdd;
        }
    }
}

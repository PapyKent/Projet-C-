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
using TodoListUCBL.BusinessServices;
using TodoListUCBL.WPFView.ModeleVue;
using TodoListUCBL.WPFView.Tools;

namespace TodoListUCBL.WPFView.Vue
{
    /// <summary>
    /// Logique d'interaction pour VisuCategory.xaml
    /// </summary>
    public partial class VisuCategory : Window
    {
        public VisuCategory()
        {
            InitializeComponent();
        }

        public VisuCategory(ModeleVue.VisuCategoryMV vcmv, List<BECategory> list)
            : this()
        {
            BindData(list);
            this.DataContext = vcmv;
        }


        public void BindData(List<BECategory> list)
        {
            CategoryList.ItemsSource = list;
        }

        private void deleteCategory(object sender, RoutedEventArgs e)
        {
            if(CategoryList.SelectedItem!=null)
            {
                CategoryService cat = new CategoryService();
                int idUser = (CategoryList.SelectedItem as BECategory).Utilisateur.Id;
                cat.SupprimerCategory((CategoryList.SelectedItem as BECategory).Id, (CategoryList.SelectedItem as BECategory).Utilisateur.Id);
                BindData(cat.GetCategories(idUser));
            }
        }


        private void ModifierCategory(object sender, RoutedEventArgs e)
        {
            if (CategoryList.SelectedItem != null)
            {
                AjouterCategoryMV acmv = new AjouterCategoryMV((CategoryList.SelectedItem as BECategory).Nom, (CategoryList.SelectedItem as BECategory).ParDefaut);
                AjouterCategory ac = new AjouterCategory(acmv);
                if (ac.ShowDialog() == true)
                {
                    CategoryService cat = new CategoryService();
                    int idUser = (CategoryList.SelectedItem as BECategory).Utilisateur.Id;
                    cat.ModifierCategory((CategoryList.SelectedItem as BECategory).Id, acmv.Nom, acmv.Pardefaut, idUser);
                    BindData(cat.GetCategories(idUser));
                }
            }
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

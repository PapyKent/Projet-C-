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
            List<TodoItemCat> items = new List<TodoItemCat>();
            foreach (BECategory c in list)
            {
                items.Add(new TodoItemCat() { Nom = c.Nom, Pardefaut = c.ParDefaut, Id = c.Id, User = c.Utilisateur.Id});
            }
            CategoryList.ItemsSource = items;
        }

        private void deleteCategory(object sender, RoutedEventArgs e)
        {
            if(CategoryList.SelectedItem!=null)
            {
                CategoryService cat = new CategoryService();
                int idUser = (CategoryList.SelectedItem as TodoItemCat).User;
                cat.SupprimerCategory((CategoryList.SelectedItem as TodoItemCat).Id);
                BindData(cat.GetCategories(idUser));
            }
        }


        private void ModifierCategory(object sender, RoutedEventArgs e)
        {
            if (CategoryList.SelectedItem != null)
            {
                AjouterCategoryMV acmv = new AjouterCategoryMV((CategoryList.SelectedItem as TodoItemCat).Nom, (CategoryList.SelectedItem as TodoItemCat).Pardefaut);
                AjouterCategory ac = new AjouterCategory(acmv);
                if (ac.ShowDialog() == true)
                {
                    CategoryService cat = new CategoryService();
                    int idUser = (CategoryList.SelectedItem as TodoItemCat).User;
                    cat.ModifierCategory((CategoryList.SelectedItem as TodoItemCat).Id, acmv.Nom, acmv.Pardefaut, idUser);
                    BindData(cat.GetCategories(idUser));
                }
            }
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class TodoItemCat
    {
        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }


        private bool pardefaut;

        public bool Pardefaut
        {
            get { return pardefaut; }
            set { pardefaut = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int user;

        public int User
        {
            get { return user; }
            set { user = value; }
        }

       public override string ToString()
        {
            return nom;
        }
    }
}

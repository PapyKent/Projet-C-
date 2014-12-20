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
                items.Add(new TodoItemCat() { Nom = c.Nom, Pardefaut = c.ParDefaut });
            }
            CategoryList.ItemsSource = items;
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
    }
}

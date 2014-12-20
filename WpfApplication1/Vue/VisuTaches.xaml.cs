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
    /// Logique d'interaction pour VisuTaches.xaml
    /// </summary>
    public partial class VisuTaches : Window
    {
        
        public VisuTaches()
        {
           
            InitializeComponent();
         
        }
       
        
       public VisuTaches(ModeleVue.VisuTachesMV vtmv, List<BETache> list) : this()
        {
            
            BindData(list);
            this.DataContext = vtmv;  
     

        }

       private void BindData(List<BETache> list)
       {
            List<TodoItem> items = new List<TodoItem>();
           foreach( BETache be in list)
           {
               items.Add(new TodoItem() { Title = be.Nom , Id= be.Id});             
           }       
           TachesList.ItemsSource = items;
       }
       private void DeleteButton_Click(object sender, RoutedEventArgs e)
       {
           this.DialogResult = true;
           this.Close();
       }

    }

    public class TodoItem
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }

}

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
    /// Logique d'interaction pour VisuTaches.xaml
    /// </summary>
    public partial class VisuTaches : Window
    {
        private int idUserEnCours;
        public VisuTaches()
        {
           
            InitializeComponent();
         
        }
       
        
       public VisuTaches(ModeleVue.VisuTachesMV vtmv, List<BETache> list,int id) : this()
        {
            idUserEnCours = id;
            BindData(list);
            this.DataContext = vtmv;  
     

        }

       private void BindData(List<BETache> list)
       {
            List<TodoItem> items = new List<TodoItem>();
           foreach( BETache be in list)
           {
               items.Add(new TodoItem() { Title = be.Nom , Id= be.Id, User=be.Utilisateur.Id, Deb=be.Debut, Fin=be.Fin, Detail=be.Detail});             
           }       
           
           TachesList.ItemsSource = items;
       }


       private void DeleteButton_Click(object sender, RoutedEventArgs e)
       {
           TacheService ts = new TacheService();
          
               ts.supprimerTache((TachesList.SelectedItem as TodoItem).Id);
               int idUser = (TachesList.SelectedItem as TodoItem).User;
               BindData(ts.VisualiserTache(idUser));
           
       }


       private void RechercheButton_Click(object sender, RoutedEventArgs e)
       {
           TacheService ts = new TacheService();

           if (string.IsNullOrWhiteSpace(this.RechercheContent.Text))
           {
               
               BindData(ts.VisualiserTache(idUserEnCours));
           }

           else
           {
               List<BETache> tmp = new List<BETache>();
               tmp = ts.rechercherTache(this.RechercheContent.Text, idUserEnCours);
               BindData(tmp);
           }

       }

       private void RetardButton_Click(object sender, RoutedEventArgs e)
       {
           TacheService ts = new TacheService();
               List<BETache> tmp = new List<BETache>();
               tmp = ts.rechercherRetardTache(idUserEnCours);
               BindData(tmp);
           

       }

       private void TrieButton_Click(object sender, RoutedEventArgs e)
       {

           TacheService ts = new TacheService();
           List<BETache> tmp = new List<BETache>();

           //Là faut passer une liste de categories->direction tacheService -> trieTache
           tmp = ts.trieTache(idUserEnCours);
           BindData(tmp);

       }


       private void ModifyButton_Click(object sender, RoutedEventArgs e)
       {
           AjouterTacheMV atmv = new AjouterTacheMV((TachesList.SelectedItem as TodoItem).Id, (TachesList.SelectedItem as TodoItem).Title, (TachesList.SelectedItem as TodoItem).Deb, (TachesList.SelectedItem as TodoItem).Fin, (TachesList.SelectedItem as TodoItem).Detail);
           AjouterTache at = new AjouterTache(atmv);
           if (at.ShowDialog() == true)
           {
               TacheService tache = new TacheService();
               int idUser = (TachesList.SelectedItem as TodoItem).User;
               tache.modifierTache((TachesList.SelectedItem as TodoItem).Id, atmv.Nom, atmv.Detail, atmv.Debut, atmv.Fin, idUser);
               BindData(tache.VisualiserTache(idUser));
           }
       }


    }

    public class TodoItem
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Id { get; set; }
        public int User { get; set; }
        public DateTime Deb { get; set; }
        public DateTime Fin { get; set; }
    }

}

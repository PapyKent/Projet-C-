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
           TachesList.ItemsSource = list;
       }


       private void DeleteButton_Click(object sender, RoutedEventArgs e)
       {
           if (this.TachesList.SelectedItem != null)
           {
               TacheService ts = new TacheService();

               ts.supprimerTache((TachesList.SelectedItem as BETache).Id);
               int idUser = (TachesList.SelectedItem as BETache).Utilisateur.Id;
               BindData(ts.VisualiserTache(idUser));
           }
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
           tmp = ts.trieTache(idUserEnCours);
           BindData(tmp);

       }


       private void ModifyButton_Click(object sender, RoutedEventArgs e)
       {
           if(this.TachesList.SelectedItem!=null)
           {
               CategoryService cat = new CategoryService();
               AjouterTacheMV atmv = new AjouterTacheMV((TachesList.SelectedItem as BETache).Id, (TachesList.SelectedItem as BETache).Nom, (TachesList.SelectedItem as BETache).Debut, (TachesList.SelectedItem as BETache).Fin, (TachesList.SelectedItem as BETache).Detail);
               AjouterTache at = new AjouterTache(atmv, cat.GetCategories((TachesList.SelectedItem as BETache).Utilisateur.Id));
               if (at.ShowDialog() == true)
               {
                   TacheService tache = new TacheService();
                   int idUser = (TachesList.SelectedItem as BETache).Utilisateur.Id;
                   tache.modifierTache((TachesList.SelectedItem as BETache).Id, atmv.Nom, atmv.Detail, atmv.Debut, atmv.Fin, idUser);
                   BindData(tache.VisualiserTache(idUser));
               }
           }
       }

       private void Retour_Click(object sender, RoutedEventArgs e)
       {
           this.Close();
       }
    }
}

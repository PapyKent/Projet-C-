using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessServices;
using TodoListUCBL.WPFView.Tools;
using TodoListUCBL.WPFView.Vue;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class ApplicationMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public ApplicationMV()
        {
            this.ConnectionInfos = new ConnectionInfosMV();
            this.AjouterTacheCommand = new CommandImpl(this.ExecuteAjouterTache, this.CanExecuteAjouterTache);
            this.AfficheCommand = new CommandImpl(this.ExecuteAffiche, this.CanExecuteAffiche);
            this.AjouterCategoryCommand = new CommandImpl(this.ExecuteAjouterCategory, this.CanExecuteAjouterCategory);
            this.VisualiserCategoryCommand = new CommandImpl(this.ExecuteVisualiserCategory, this.CanExecuteVisualiserCategory);
        }

        private ConnectionInfosMV connectionInfos;

        public ConnectionInfosMV ConnectionInfos
        {
            get
            {
                return connectionInfos;
            }
            set
            {
                if (connectionInfos != value)
                {
                    connectionInfos = value;
                    RaisePropertyChanged("ConnectionInfos");
                }
            }
        }

        #region AjouterTache Command

        public CommandImpl AjouterTacheCommand
        {
            get;
            private set;
        }


        private void ExecuteAjouterTache(object o)
        {
            AjouterTacheMV atmv = new AjouterTacheMV();
            AjouterTache at = new AjouterTache(atmv);
            if(at.ShowDialog() == true)
            {
                TacheService tache = new TacheService();
                tache.AjouterTache(this.ConnectionInfos.UtilisateurConnecte.Id, atmv.Nom, atmv.Debut, atmv.Fin, atmv.Detail);
            }
        }

        private bool CanExecuteAjouterTache(object o)
        {
            return true;
        }

        #endregion 

        #region AjouterCategory Command

        public CommandImpl AjouterCategoryCommand
        {
            get;
            private set;
        }


        private void ExecuteAjouterCategory(object o)
        {
            AjouterCategoryMV acmv = new AjouterCategoryMV();
            AjouterCategory ac = new AjouterCategory(acmv);
            if (ac.ShowDialog() == true)
            {
                CategoryService cat = new CategoryService();
                cat.AjouterCategory(this.connectionInfos.UtilisateurConnecte.Id, acmv.Nom, acmv.Pardefaut);
            }
        }

        private bool CanExecuteAjouterCategory(object o)
        {
            return true;
        }

        #endregion

        #region Affiche Command
        public CommandImpl AfficheCommand
        {
            get;
            private set;
        }


        private void ExecuteAffiche(object o)
        {
            
           
            VisuTachesMV vtmv = new VisuTachesMV();
            TacheService tache = new TacheService();
            VisuTaches vt = new VisuTaches(vtmv, tache.VisualiserTache(this.ConnectionInfos.UtilisateurConnecte.Id));
           
            if (vt.ShowDialog() == true)
            {
                
               
               
              }

        }

        private bool CanExecuteAffiche(object o)
        {
            return true;
        }
        #endregion

        #region VisualiserCategory Command

        public CommandImpl VisualiserCategoryCommand
        {
            get;
            private set;
        }

        private void ExecuteVisualiserCategory(object o)
        {
            CategoryService cat = new CategoryService();
            VisuCategoryMV vcmv = new VisuCategoryMV();
            VisuCategory vc = new VisuCategory(vcmv, cat.GetCategories(this.ConnectionInfos.UtilisateurConnecte.Id));
            vc.ShowDialog();
        }

        private bool CanExecuteVisualiserCategory(object o)
        {
            return true;
        }

        #endregion

        
    }
}

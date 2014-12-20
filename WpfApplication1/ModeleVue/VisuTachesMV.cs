using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.WPFView.Tools;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class VisuTachesMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            /*List<BETache> tache;
            tache.Add();
            tache[0].Nom;*/
        }

        public VisuTachesMV()
        {
            
         }


        private string nom;
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                if (nom != value)
                {
                    nom = value;
                    // this.RemoveTask.RequerySuggested();
                    RaisePropertyChanged("Name");
                }
            }
        }





    }
}

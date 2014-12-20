using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListUCBL.WPFView.ModeleVue
{
    
    public class AjouterTacheMV : INotifyPropertyChanged
    {
       

        public AjouterTacheMV()
        {
           
        }
        public AjouterTacheMV(int id,string nom, DateTime debut, DateTime fin, string detail)
        {
        
            this.nom = nom;
            this.debut = debut;
            this.fin = fin;
            this.detail = detail;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string nom;
        private DateTime debut = DateTime.Now;
        private DateTime fin = DateTime.Now;
        private string detail;

        public string Detail
        {
            get { return detail; }
            set 
            { 
                if(detail!=value)
                {
                    detail = value;
                    RaisePropertyChanged("Detail");
                }
            }
        }

        public DateTime Fin
        {
            get { return fin; }
            set
            { 
                if(fin!=value)
                {
                    fin = value;
                    RaisePropertyChanged("Fin");
                } 
            }
        }

        public DateTime Debut
        {
            get { return debut; }
            set 
            {
                if(debut!=value)
                {
                    debut = value;
                    RaisePropertyChanged("Debut");
                }
            }
        }

        public string Nom
        {
            get { return nom; }
            set 
            { 
                if(nom!=value)
                {
                    nom = value;
                    if(string.IsNullOrWhiteSpace(nom))
                    {
                        throw new Exception("Le champ est obligatoire");
                    }
                    else
                    {
                        RaisePropertyChanged("Nom");
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class AjouterCategoryMV : INotifyPropertyChanged
    {
        public AjouterCategoryMV()
        {
        }
        public AjouterCategoryMV(string nom, bool pardefaut)
        {
            this.Nom = nom;
            this.Pardefaut = pardefaut;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string nom;

        public string Nom
        {
            get { return nom; }
            set 
            { 
                if(nom != value)
                {
                    nom = value;
                    if (string.IsNullOrWhiteSpace(nom))
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
        private bool pardefaut;

        public bool Pardefaut
        {
            get { return pardefaut; }
            set 
            { 
                if(pardefaut != value)
                {
                    pardefaut = value;
                    RaisePropertyChanged("ParDefaut");
                }
            }
        } 
    }
}

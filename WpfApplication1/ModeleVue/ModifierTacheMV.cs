using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class ModifierTacheMV
    {
        private List<BECategory> categsUsed;

        public List<BECategory> CategsUsed
        {
            get { return categsUsed; }
            set { categsUsed = value; }
        }

        private BEEtat etat;

        public BEEtat Etat
        {
            get { return etat; }
            set { etat = value; }
        }

        public ModifierTacheMV(int id, string nom, DateTime debut, DateTime fin, string detail, List<BECategory> alreadyUsed, BEEtat etat)
        {
            this.Etat = etat;
            this.id = id;
            this.nom = nom;
            this.debut = debut;
            this.fin = fin;
            this.detail = detail;
            this.categsUsed = new List<BECategory>();
            foreach(BECategory cat in alreadyUsed)
            {
                this.categsUsed.Add(cat);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
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

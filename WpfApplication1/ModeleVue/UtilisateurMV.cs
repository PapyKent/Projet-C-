using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class UtilisateurMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string nom;
        private string prenom;
        private string email;
        private string login;
        private string password;
        private int id;

        public int Id
        {
            get { return id; }
            set 
            {
                if(id!=value)
                {
                    id = value;
                }
            }
        }


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
                    if(string.IsNullOrWhiteSpace(nom) == true)
                    {
                        throw new Exception("Le champ nom est obligatoire.");
                    }
                    RaisePropertyChanged("Nom");
                }
            }
        }
        public string Prenom  
        { 
            get
            {
                return prenom;
            }
            set
            {
                if (prenom != value)
                {
                    prenom = value;
                    if (string.IsNullOrWhiteSpace(prenom) == true)
                    {
                        throw new Exception("Le champ prenom est obligatoire.");
                    }
                    RaisePropertyChanged("Prenom");
                }
            }
        }
        public string Email  
        { 
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    if (string.IsNullOrWhiteSpace(email) == true)
                    {
                        throw new Exception("Le champ email est obligatoire.");
                    }

                    RaisePropertyChanged("Email");
                }
            }
        }
        public virtual string Login  
        { 
            get
            {
                return login;
            }
            set
            {
                if (login != value)
                {
                    login = value;
                    if (string.IsNullOrWhiteSpace(login) == true)
                    {
                        throw new Exception("Le champ login est obligatoire.");
                    }
                    RaisePropertyChanged("Login");
                }
            }
        }

        public string Password  
        { 
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    if (string.IsNullOrWhiteSpace(password) == true)
                    {
                        throw new Exception("Le champ password est obligatoire.");
                    }

                    if (password.Length < 8)
                    {
                        throw new Exception("Le champ password doit contenir au moins 8 caractères.");
                    }

                    RaisePropertyChanged("Password");
                }
            }
        }

    }
}

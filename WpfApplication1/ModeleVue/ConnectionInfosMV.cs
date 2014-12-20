using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.BusinessServices;
using TodoListUCBL.WPFView.Tools;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class ConnectionInfosMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public ConnectionInfosMV()
        {
            this.SeConnecterCommand = new CommandImpl(ExecuteSeConnecter,CanExecuteSeConnecter);
            this.SinscrireCommand = new CommandImpl(ExecuteSinscrire,CanExecuteSinscrire);

            this.AnnulerSinscrireCommand = new CommandImpl(ExecuteAnnulerSinscrire,CanExecuteAnnulerSinscrire);

            this.ValiderInscriptionCommand = new CommandImpl(ExecuteValiderInscription,CanExecuteValiderInscription);
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                if(errorMessage != value)
                {
                    errorMessage = value;
                    RaisePropertyChanged("ErrorMessage");
                }
            }
        }

        private AuthentificationModeEnum authentificationMode = AuthentificationModeEnum.Connexion;
        public AuthentificationModeEnum AuthentificationMode
        {
            get
            {
                return authentificationMode;
            }
            set
            {
                if (authentificationMode != value)
                {
                    authentificationMode = value;
                    RaisePropertyChanged("AuthentificationMode");
                }
            }
        }

        private void ActualiserAuthentificationMode()
        {
            if (this.UtilisateurConnecte != null)
            {
                this.AuthentificationMode = AuthentificationModeEnum.Connecte;
            }
            else if (this.NouvelUtilisateur != null)
            {
                this.AuthentificationMode = AuthentificationModeEnum.Inscription;
            }
            else
            {
                this.AuthentificationMode = AuthentificationModeEnum.Connexion;
            }
        }

        private string login;
        public string Login
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
                    this.SeConnecterCommand.RequerySuggested();
                    RaisePropertyChanged("Login");
                }
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if(password != value)
                {
                    password = value;
                    this.SeConnecterCommand.RequerySuggested();
                    RaisePropertyChanged("Password");
                }
            }
        }

        private NouvelUtilisateurMV nouvelUtilisateur;
        public NouvelUtilisateurMV NouvelUtilisateur
        {
            get
            {
                return nouvelUtilisateur;
            }
            set
            {
                if (nouvelUtilisateur  != value)
                {
                    nouvelUtilisateur = value;
                    ActualiserAuthentificationMode();
                    RaisePropertyChanged("NouvelUtilisateur");
                }
            }
        }

        private UtilisateurMV utilisateurConnecte;
        public UtilisateurMV UtilisateurConnecte
        {
            get
            {
                return utilisateurConnecte;
            }
            set
            {
                if (utilisateurConnecte != value)
                {
                    utilisateurConnecte = value;
                    ActualiserAuthentificationMode();
                    RaisePropertyChanged("UtilisateurConnecte");
                }
            }
        }

        #region se connecter Command
        public CommandImpl SeConnecterCommand
        {
            get;
            private set;
        }


        private void ExecuteSeConnecter(object o)
        {
            ErrorMessage = null;
            UtilisateurService utilisateurService = new UtilisateurService();
            try
            {
                BEUtilisateur utilisateur = utilisateurService.Authentification(this.Login, this.Password);
                if (utilisateur != null)
                {
                    UtilisateurMV utilisateurConnecte = new UtilisateurMV()
                    {
                        Id = utilisateur.Id,
                        Email = utilisateur.Email,
                        Login = utilisateur.Login,
                        Nom = utilisateur.Nom,
                        Prenom = utilisateur.Prenom
                    };
                    this.UtilisateurConnecte = utilisateurConnecte;
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private bool CanExecuteSeConnecter(object o)
        {

            return string.IsNullOrWhiteSpace(this.Login) == false 
                && string.IsNullOrWhiteSpace(this.Password)==false;
        }

        #endregion

        #region S'incrire Command
        public CommandImpl SinscrireCommand
        {
            get;
            private set;
        }


        private void ExecuteSinscrire(object o)
        {
            this.NouvelUtilisateur = new NouvelUtilisateurMV();
        }

        private bool CanExecuteSinscrire(object o)
        {
            return true;
        }
        #endregion

        #region Annuler S'incrire Command
        public CommandImpl AnnulerSinscrireCommand
        {
            get;
            private set;
        }


        private void ExecuteAnnulerSinscrire(object o)
        {
            this.NouvelUtilisateur = null;
        }

        private bool CanExecuteAnnulerSinscrire(object o)
        {
            return true;
        }
        #endregion
        
        #region ValiderInscription Command

        public CommandImpl ValiderInscriptionCommand
        {
            get;
            private set;
        }


        private void ExecuteValiderInscription(object o)
        {
            UtilisateurService utilisateurService = new UtilisateurService();
            try
            {
                BEUtilisateur utilisateurAjoutée = utilisateurService.AjouterUtilisateur(this.NouvelUtilisateur.Nom, this.NouvelUtilisateur.Prenom, this.NouvelUtilisateur.Email, this.NouvelUtilisateur.Login, this.NouvelUtilisateur.Password);

                this.NouvelUtilisateur = null;
            }
            catch(Exception ex)
            {
                this.ErrorMessage = "Une erreur est survenue lors de l'inscription : " + ex.Message;
            }
        }

        private bool CanExecuteValiderInscription(object o)
        {
            return true;
        }

        #endregion

        


    }
}

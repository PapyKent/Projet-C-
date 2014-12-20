using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessServices;

namespace TodoListUCBL.WPFView.ModeleVue
{
    public class NouvelUtilisateurMV : UtilisateurMV
    {
        public override string Login
        {
            get
            {
                return base.Login;
            }
            set
            {
                if (base.Login != value)
                {
                    UtilisateurService utilisateurService = new UtilisateurService();
                    base.Login = value;
                }
            }
        }
        private string password2;
        public string Password2
        {
            get
            {
                return password2;
            }
            set
            {
                if (password2 != value)
                {
                    password2 = value;
                    RaisePropertyChanged("Password2");
                    if (password2 != Password)
                    {
                        throw new Exception("Le mot de passe n'est pas identique.");
                    }
                }
            }
        }
    }
}

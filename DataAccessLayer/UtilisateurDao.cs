using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.EntityFramework;

namespace TodoListUCBL.DataAccessLayer
{
    /// <summary>
    /// Classe permettant d'intérgoer la base de données. 
    /// S'assure de l'intégrité de la table utilisateur.
    /// Filtre les données qui remonte de la base de données
    /// </summary>
    public class UtilisateurDao
    {
        /// <summary>
        /// Permet d'ajouter un nouvel utilisateur
        /// </summary>
        /// <param name="nom">Le nom du nouvel utilisateur</param>
        /// <param name="prenom">Le prénom du nouvel utilisateur</param>
        /// <param name="email">L'email du nouvel utilisateur</param>
        /// <param name="login">Le login du nouvel utilisateur</param>
        /// <param name="motdepasse">Le mot de passe du nouvel utilisateur</param>
        /// <returns>L'utilisateur ajouté</returns>
        public BEUtilisateur AjouterUtilisateur(string nom,string prenom,string email,string login,string motdepasse)
        {
            //première étape : On verifie les données
            if(string.IsNullOrWhiteSpace(login) == true)
            {
                throw new ArgumentException("Veuillez renseigner le login.", "login");
            }
            if (string.IsNullOrWhiteSpace(motdepasse) == true)
            {
                throw new ArgumentException("Le mot de passe est obligatoire.", "motdepasse");
            }

            //deuxième étape enregistrement

            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Utilisateur u = new Utilisateur();
                u.Nom = nom;
                u.Prenom = prenom;
                u.Email = email;
                u.Login = login;
                u.Password = motdepasse;

                context.UtilisateurSet.Add(u);

                context.SaveChanges();

                //3eme etape on retourne
                BEUtilisateur ret = new BEUtilisateur();


                ret.Id = u.Id;
                ret.Nom = u.Nom;
                ret.Prenom = u.Prenom;
                ret.Email = u.Email;
                ret.Login = u.Login;
                //sensible on fait en sorte qu'il ne parte jamais d'ici
                ret.Password = null;

                return ret;
            }
        }

        /// <summary>
        /// Permet de récupérer un utilisateur si les infos de connexion sont correctes
        /// </summary>
        /// <param name="login">Le login</param>
        /// <param name="password">Le mot de passe</param>
        /// <returns>L'utilisateur si celui-ci est authentifié</returns>
        public BEUtilisateur Authentification(string login,string password)
        {
            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                //on requete
                Utilisateur user = context.UtilisateurSet.FirstOrDefault(u => u.Login == login && u.Password == password); 
 
                //on retourne
                BEUtilisateur ret = null;
                if(user != null)
                {
                    ret = new BEUtilisateur();
                    ret.Id = user.Id;
                    ret.Nom = user.Nom;
                    ret.Prenom = user.Prenom;
                    ret.Email = user.Email;
                    ret.Login = user.Login;
                    //sensible on fait en sorte qu'il ne parte jamais d'ici
                    ret.Password = null;
                        
                }

                return ret;
            }
        }


        /// <summary>
        /// Verifie si l'adresse email existe déjà dans la BDD
        /// </summary>
        /// <param name="email">l'adresse email à verfier</param>
        /// <returns>true si l'adresse existe</returns>
        public bool AdresseEmailExisteDeja(string email)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                return context.UtilisateurSet.Any(u => u.Email == email);
            }
        }

        /// <summary>
        /// Verifie si le login existe déjà dans la BDD
        /// </summary>
        /// <param name="email">le login à verfier</param>
        /// <returns>true si le login existe</returns>
        public bool LoginExisteDeja(string login)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                return context.UtilisateurSet.Any(u => u.Login == login);
            }
        }
    }
}

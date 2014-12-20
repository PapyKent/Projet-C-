using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.DataAccessLayer;

namespace TodoListUCBL.BusinessServices
{
    /// <summary>
    /// Cette classe à pour objectif d'exposer les services métiers associé à l'objet utilisateur
    /// En charge de la partie sécurité et regles métiers
    /// </summary>
    public class UtilisateurService
    {
        /// <summary>
        /// Permet d'ajouter un nouvel utilisateur en base de données
        /// </summary>
        /// <param name="nom">Le nom du nouvel utilisateur</param>
        /// <param name="prenom">Le prénom du nouvel utilisateur</param>
        /// <param name="email">L'email du nouvel utilisateur</param>
        /// <param name="login">Le login du nouvel utilisateur</param>
        /// <param name="motdepasse">Le mot de passe du nouvel utilisateur</param>
        /// <returns>L'utilisateur ajouté</returns>
        public BEUtilisateur AjouterUtilisateur(string nom,string prenom,string email,string login,string motdepasse)
        {
            UtilisateurDao utilisateurDao = new UtilisateurDao();
            //on va verifier que l'utilisateur n'existe pas déjà 
            
            //on verifie l'email
            if(utilisateurDao.AdresseEmailExisteDeja(email)==true)
            {
                throw new ArgumentException("L'adresse mail existe déjà.", "email");
            }
            //on verifie le login
            if (utilisateurDao.LoginExisteDeja(login) == true)
            {
                throw new ArgumentException("Le login existe déjà.", "email");
            }
            //on ajoute
            BEUtilisateur utilisateur =  null;
            try
            {
                utilisateur = utilisateurDao.AjouterUtilisateur(nom, prenom, email, login, motdepasse);
                if(utilisateur == null)
                {
                    throw new Exception("L'utilisateur est null.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de l'enregistrement.", ex);
            }

            //on retourne
            return utilisateur;
        }

        /// <summary>
        /// Permet de récupérer un utilisateur si les infos de connexion sont correctes
        /// </summary>
        /// <param name="login">Le login</param>
        /// <param name="password">Le mot de passe</param>
        /// <returns>L'utilisateur si celui-ci est authentifié</returns>
        public BEUtilisateur Authentification(string login,string motdepasse)
        {
            UtilisateurDao utilisateurDao = new UtilisateurDao();
            BEUtilisateur utilisateur = null;
            try
            {
                utilisateur = utilisateurDao.Authentification(login, motdepasse);
                
            }
            catch(Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de l'authentification.", ex);
            }

            if (utilisateur == null)
            {
                throw new Exception("L'utilisateur n'existe pas.");
            }
            return utilisateur;
        }
    }
}

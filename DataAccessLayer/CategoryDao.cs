using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.EntityFramework;

namespace TodoListUCBL.DataAccessLayer
{
    public class CategoryDao
    {
        public BECategory AjouterCategory(int id, string nom, bool pardefaut)
        {
            if(string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner la category.", "nom");
            }

            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category c = new Category();
                c.Id = id;
                c.Nom = nom;
                c.ParDefaut = pardefaut;
                c.Utilisateur = context.UtilisateurSet.First(u => u.Id == id);

                context.CategorySet.Add(c);

                context.SaveChanges();

                BECategory retour = new BECategory();

                retour.Id = c.Id;
                retour.Nom = c.Nom;
                retour.ParDefaut = c.ParDefaut;

                BEUtilisateur user = new BEUtilisateur();
                user.Email = c.Utilisateur.Email;
                user.Id = c.Utilisateur.Id;
                user.Login = c.Utilisateur.Login;
                user.Password = c.Utilisateur.Password;
                user.Prenom = c.Utilisateur.Prenom;
                retour.Utilisateur = user;

                return retour;
            }
        }

        public bool SupprimerCategory(int id)
        {
            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category cat = context.CategorySet.FirstOrDefault(c => c.Id == id);
                if(cat == null)
                {
                    throw new ArgumentException("Veuillez renseigner une catégorie existante.", "id");
                }
                else
                {
                    context.CategorySet.Remove(cat);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public bool CateogryExisteDeja(int id)
        {
            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                return context.CategorySet.Any(c => c.Id == id);
            }
        }
    }
}

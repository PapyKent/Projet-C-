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
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner la category.", "nom");
            }

            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category c = new Category();
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

        public BECategory ModifierCategory(int idCat, string nom, bool pardef, int idUser)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner la category.", "nom");
            }

            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category c = context.CategorySet.FirstOrDefault(cat => cat.Id == idCat);
                if(nom!=c.Nom)
                {
                    c.Nom = nom;
                }
                if(pardef!=c.ParDefaut)
                {
                    c.ParDefaut = pardef;
                }

                context.SaveChanges();

                BECategory retour = new BECategory();
                retour.Id = c.Id;
                retour.Nom = c.Nom;
                retour.ParDefaut = c.ParDefaut;

                BEUtilisateur user = new BEUtilisateur();
                user.Id = idUser;

                retour.Utilisateur = user;

                return retour;
            }
        }

        public bool SupprimerCategory(int id)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category cat = context.CategorySet.FirstOrDefault(c => c.Id == id);
                if (cat == null)
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
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                return context.CategorySet.Any(c => c.Id == id);
            }
        }

        public List<BECategory> GetCategories(int idUser)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                List<BECategory> retour = new List<BECategory>();
                IQueryable<Category> list = context.CategorySet.Where(c => c.Utilisateur.Id == idUser);
                foreach (Category c in list)
                {
                    BECategory cat = new BECategory();
                    cat.Id = c.Id;
                    cat.Nom = c.Nom;
                    cat.ParDefaut = c.ParDefaut;

                    //BEUtilisateur user = new BEUtilisateur();
                    //user.Email = cat.Utilisateur.Email;
                    //user.Id = cat.Utilisateur.Id;
                    //user.Login = cat.Utilisateur.Login;
                    //user.Password = cat.Utilisateur.Password;
                    //user.Prenom = cat.Utilisateur.Prenom;
                    //cat.Utilisateur = user;

                    Utilisateur u = context.UtilisateurSet.First(us => us.Id == idUser);
                    BEUtilisateur user = new BEUtilisateur();
                    user.Id = u.Id;

                    cat.Utilisateur = user;

                    //foreach (BETache t in cat.Taches)
                    //{
                    //    cat.Taches.Add(t);
                    //}

                    retour.Add(cat);
                }
                return retour;
            }
        }
    }
}

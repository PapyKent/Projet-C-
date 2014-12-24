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
        public BECategory AjouterCategory(int idUser, string nom, bool pardefaut)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner la category.", "nom");
            }

            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category c = new Category();
                c.Nom = nom;
                if (context.CategorySet.Include("Utilisateur").Where(cat => cat.Utilisateur.Id == idUser).ToList().Count() == 0)
                {
                    c.ParDefaut = true;
                }
                else
                {
                    c.ParDefaut = pardefaut;
                }
                
                c.Utilisateur = context.UtilisateurSet.First(u => u.Id == idUser);

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
                    if (context.CategorySet.Include("Utilisateur").Where(cat => cat.Utilisateur.Id == idUser).ToList().Count() == 1)
                    {
                        c.ParDefaut = true;
                    }
                    else
                    {
                        c.ParDefaut = pardef;
                    }                    
                }

                List<Category> LastCateg = context.CategorySet.Include("Taches").Where(cc => cc.Utilisateur.Id == idUser && cc.ParDefaut == true).ToList();

                if (LastCateg.Count() == 1)
                {
                    Category categ = context.CategorySet.First(nomdevariabletrèschiantadefinirsansconflit => nomdevariabletrèschiantadefinirsansconflit.Utilisateur.Id == idUser && nomdevariabletrèschiantadefinirsansconflit.ParDefaut == false);
                    categ.ParDefaut = true;
                }

                context.SaveChanges();

                BECategory retour = new BECategory();
                retour.Id = c.Id;
                retour.Nom = c.Nom;
                retour.ParDefaut = c.ParDefaut;

                Utilisateur u = context.UtilisateurSet.FirstOrDefault(us => us.Id == idUser);

                BEUtilisateur user = new BEUtilisateur();
                user.Id = u.Id;
                user.Login = u.Login;
                user.Nom = u.Nom;
                user.Password = u.Password;
                user.Prenom = u.Prenom;
                user.Email = u.Email;

                retour.Utilisateur = user;

                return retour;
            }
        }

        public bool SupprimerCategory(int id, int idUser)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Category cat = context.CategorySet.Include("Taches").FirstOrDefault(c => c.Id == id);
                if (cat == null)
                {
                    throw new ArgumentException("Veuillez renseigner une catégorie existante.", "id");
                }
                else
                {
                    foreach(Tache t in cat.Taches)
                    {
                        t.Categories.Remove(cat);
                        
                    }
                    context.CategorySet.Remove(cat);

                    List<Category> LastCateg = context.CategorySet.Include("Taches").Where(c => c.Utilisateur.Id == idUser && c.ParDefaut==true).ToList();

                    if (LastCateg.Count() == 1)
                    {
                        Category c = context.CategorySet.First(nomdevariabletrèschiantadefinirsansconflit => nomdevariabletrèschiantadefinirsansconflit.Utilisateur.Id == idUser && nomdevariabletrèschiantadefinirsansconflit.ParDefaut == false);
                        c.ParDefaut = true;
                    }

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
                Utilisateur userBD = context.UtilisateurSet.First(us => us.Id == idUser);
                BEUtilisateur user = new BEUtilisateur();
                user.Id = userBD.Id;
                user.Login = userBD.Login;
                user.Nom = userBD.Nom;
                user.Password = userBD.Password;
                user.Prenom = userBD.Prenom;
                user.Email = userBD.Email;

                List<BECategory> retour = new List<BECategory>();
                List<Category> list = context.CategorySet.Include("Utilisateur").Include("Taches").Where(c => c.Utilisateur.Id == idUser).ToList();

                foreach (Category c in list)
                {
                    BECategory cat = new BECategory();
                    cat.Id = c.Id;
                    cat.Nom = c.Nom;
                    cat.ParDefaut = c.ParDefaut;
                    cat.Utilisateur = user;

                    foreach(Tache t in c.Taches)
                    {
                        BETache tache = new BETache();
                        tache.Id = t.Id;
                        tache.Nom = t.Nom;
                        tache.Debut = t.Debut;
                        tache.Fin = t.Fin;
                        tache.Detail = t.Detail;

                        cat.Taches.Add(tache);
                    }

                    retour.Add(cat);
                }
                return retour;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.EntityFramework;

namespace TodoListUCBL.DataAccessLayer
{
    public class TacheDao
    {
        public BETache AjouterTache(int idUser, string nom, System.DateTime debut, System.DateTime fin, string detail)
        {
            //Contrôle
            if(string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner le nom de la tache.", "nom");
            }
            if (string.IsNullOrWhiteSpace(detail))
            {
                throw new ArgumentException("Veuillez renseigner le detail de la tache.", "detail");
            }
            if( fin < debut)
            {
                throw new ArgumentException("Veuillez renseigner une date de fin valide.", "fin");
            }

            //Enregistrement

            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Tache t = new Tache();
                t.Nom = nom;
                t.Debut = debut;
                t.Fin = fin;
                t.Detail = detail;
                t.Etat = context.EtatSet.Add(context.EtatSet.First(c => c.Code == "007"));              
                t.Utilisateur = context.UtilisateurSet.First(u => u.Id == idUser);

                    try
                    {
                        //t.Categories.Add(context.CategorySet.First(cat => cat.Id == ??));
                    }
                    catch (Exception e)
                    {
                        // ?
                    }

                context.TacheSet.Add(t);
                context.SaveChanges();

                //On retourne la tache

                BETache retour = new BETache();

                retour.Id = t.Id;
                retour.Nom = t.Nom;
                retour.Debut = t.Debut;
                retour.Fin = t.Fin;
                retour.Detail = t.Detail;

                BEEtat etat = new BEEtat();
                etat.Id = t.Etat.Id;
                etat.Libelle = t.Etat.Libelle;
                etat.Code = t.Etat.Code;

                retour.Etat = etat;

                foreach(Category cat in t.Categories)
                {
                    BECategory categ = new BECategory();
                    categ.Id = cat.Id;
                    categ.Nom = cat.Nom;
                    categ.ParDefaut = cat.ParDefaut;

                    retour.Categories.Add(categ);
                }

                BEUtilisateur user = new BEUtilisateur();

                user.Email = t.Utilisateur.Email;
                user.Id = t.Utilisateur.Id;
                user.Login = t.Utilisateur.Login;
                user.Password = t.Utilisateur.Password;
                user.Prenom = t.Utilisateur.Prenom;
                user.Nom = t.Utilisateur.Nom;

                retour.Utilisateur = user;

                return retour;
            }
        }

        public bool supprimerTache(int id)
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                //Si la tache existe
                Tache tache = context.TacheSet.FirstOrDefault(t => t.Id == id);
                if(tache == null)
                {
                    throw new ArgumentException("Veuillez renseigner une tache existante.", "id");
                }
                else
                {
                    context.TacheSet.Remove(tache);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public BETache modifierTache(int id, string nom, string detail, DateTime debut, DateTime fin, int idUser)
        {
            //Contrôle
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Veuillez renseigner le nom de la tache.", "nom");
            }
            if (string.IsNullOrWhiteSpace(detail))
            {
                throw new ArgumentException("Veuillez renseigner le detail de la tache.", "detail");
            }
            if (fin < DateTime.Now || fin < debut)
            {
                throw new ArgumentException("Veuillez renseigner une date de fin valide.", "fin");
            }

            //Enregistrement 
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                Tache t = context.TacheSet.FirstOrDefault(p => p.Id == id);
                if(nom!=t.Nom)
                {
                    t.Nom = nom;
                }
                if(detail!=t.Detail)
                {
                    t.Detail = detail;
                }
                if(debut!=t.Debut)
                {
                    t.Debut = debut;
                }
                if(fin!=t.Fin)
                {
                    t.Fin = fin;
                }

                try
                {
                       // t.Categories.Add(context.CategorySet.First(c => c.Id == cat.Id));
                }
                catch (Exception e)
                {
                    //
                }

                context.SaveChanges();

                BETache retour = new BETache();
                retour.Id = t.Id;
                retour.Nom = t.Nom;
                retour.Debut = t.Debut;
                retour.Fin = t.Fin;
                retour.Detail = t.Detail;   

                foreach (Category cat in t.Categories)
                {
                    BECategory categ = new BECategory();
                    categ.Id = cat.Id;
                    categ.Nom = cat.Nom;
                    categ.ParDefaut = cat.ParDefaut;

                    retour.Categories.Add(categ);
                }

                BEEtat etat = new BEEtat();
                etat.Id = t.Etat.Id;
                etat.Libelle = t.Etat.Libelle;
                etat.Code = t.Etat.Code;

                retour.Etat = etat;

                BEUtilisateur user = new BEUtilisateur();
                user.Id = t.Utilisateur.Id;
                user.Login = t.Utilisateur.Login;
                user.Nom = t.Utilisateur.Nom;
                user.Password = t.Utilisateur.Password;
                user.Prenom = t.Utilisateur.Prenom;
                user.Email = t.Utilisateur.Email;

                retour.Utilisateur = user;

                return retour;
            }
        }
        
        public bool  TacheExisteDeja(int id)
        {
            using(TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                return context.TacheSet.Any(t => t.Id == id);
            }
        }

        public List<BETache> getTaches(int idUser)
        {
          
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                //Récup de l'utilisateur pour toute la suite
                Utilisateur userBD = context.UtilisateurSet.First(c => c.Id == idUser);
                BEUtilisateur user = new BEUtilisateur();
                user.Id = userBD.Id;
                user.Login = userBD.Login;
                user.Nom = userBD.Nom;
                user.Password = userBD.Password;
                user.Prenom = userBD.Prenom;
                user.Email = userBD.Email;
                
                //Récup de la liste des tâches liées a l'utilisateur
                List<BETache> retour = new List<BETache>();
                IQueryable<Tache> list = context.TacheSet.Where(c => c.Utilisateur.Id == idUser);
               
                foreach (Tache t in list)
                {
                    BETache tach = new BETache();
                    tach.Id = t.Id;
                    tach.Nom = t.Nom;
                    tach.Debut = t.Debut;
                    tach.Fin = t.Fin;
                    tach.Detail = t.Detail;

                    foreach(Category c in t.Categories)
                    {
                        BECategory cat = new BECategory();
                        cat.Id = c.Id;
                        cat.Nom = c.Nom;
                        cat.ParDefaut = c.ParDefaut;
                        cat.Utilisateur = user;
                    }

                    tach.Utilisateur = user;
                    retour.Add(tach);
                }
          
                return retour;
            }
        }

    }

}

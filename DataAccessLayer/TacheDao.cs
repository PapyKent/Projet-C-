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
        public BETache AjouterTache(int id, string nom, System.DateTime debut, System.DateTime fin, string detail)
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
            if(fin < DateTime.Now || fin < debut)
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
                t.Utilisateur = context.UtilisateurSet.First(u => u.Id == id);

                try
                {
                    t.Categories.Add(context.CategorySet.First(c => c.ParDefaut == true));
                }
                catch (Exception e)
                {
                    //Pas d'exception
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

                foreach(Category cat in t.Categories)
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
                user.Email = t.Utilisateur.Email;
                user.Id = t.Utilisateur.Id;
                user.Login = t.Utilisateur.Login;
                user.Password = t.Utilisateur.Password;
                user.Prenom = t.Utilisateur.Prenom;
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

                context.SaveChanges();

                BETache retour = new BETache();
                retour.Id = t.Id;
                retour.Nom = t.Nom;
                retour.Debut = t.Debut;
                retour.Fin = t.Fin;
                retour.Detail = t.Detail;

                BEEtat etat = new BEEtat();
                etat.Libelle = t.Etat.Libelle;
                etat.Code = t.Etat.Code;
                retour.Etat = etat;

                BEUtilisateur beUh = new BEUtilisateur();
                beUh.Id = idUser;

                retour.Utilisateur = beUh;

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
                Utilisateur util = context.UtilisateurSet.First(c => c.Id == idUser);
                BEUtilisateur beUtil = new BEUtilisateur();
                beUtil.Id = util.Id;
                

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

                        tach.Utilisateur = beUtil;
                        retour.Add(tach);
                    }
                   
                
                return retour;
            }
        }
    }
}

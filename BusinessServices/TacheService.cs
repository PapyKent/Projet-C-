﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.DataAccessLayer;

namespace TodoListUCBL.BusinessServices
{
    public class TacheService
    {
        public BETache AjouterTache(int id, string nom, System.DateTime debut, System.DateTime fin, string detail)
        {
            TacheDao tacheDao = new TacheDao();

            //On ajoute
            BETache tache = null;
            try
            {
                tache = tacheDao.AjouterTache(id, nom, debut, fin, detail);
                if(tache == null)
                {
                    throw new Exception("La tache est null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de l'ajout de la tache.", ex);
            }

            //On retourne 
            return tache;
        }

        public bool supprimerTache(int id)
        {
            TacheDao tacheDao = new TacheDao();

            //On vérifie si la tache existe
            if (!tacheDao.TacheExisteDeja(id))
            {
                throw new ArgumentException("La tache n'existe pas.", "id");
            }

            bool tache = false;

            try
            {
                tache = tacheDao.supprimerTache(id);
                if(tache==false)
                {
                    throw new Exception("Erreur lors de la suppression de la tache.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la suppression de la tache.", ex);
            }

            return tache;
        }

         public BETache modifierTache(int id, string nom, string detail, DateTime debut, DateTime fin)
        {
            TacheDao tacheDao = new TacheDao();

            //On vérifie si la tache existe
            if (!tacheDao.TacheExisteDeja(id))
            {
                throw new ArgumentException("La tache n'existe pas.", "id");
            }

            BETache tache = null;

            try
            {
                tache = tacheDao.modifierTache(id, nom, detail, debut, fin);
                if(tache == null)
                {
                    throw new Exception("La tache est null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la modification de la tache.", ex);
            }

            return tache;
        }

         public  List<BETache> VisualiserTache(int id)
         {
             
             TacheDao tacheDao = new TacheDao();

             List<BETache> list = new List<BETache>();
            

             try
             {
                 list=tacheDao.getTaches(id);
                 if (list ==null)
                 {
                     throw new Exception("Erreur lors de la récupération des taches.");
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("Une erreur est survenue lors de la récupération des taches", ex);
             }

             return list;
         }

    }
}
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
        public BETache AjouterTache(int id, string nom, System.DateTime debut, System.DateTime fin, string detail, List<BECategory> categories)
        {
            TacheDao tacheDao = new TacheDao();

            //On ajoute
            BETache tache = null;
            try
            {
                tache = tacheDao.AjouterTache(id, nom, debut, fin, detail, categories);
                if (tache == null)
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
                if (tache == false)
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

        public BETache modifierTache(int id, string nom, string detail, DateTime debut, DateTime fin, int idUser, List<BECategory> categories, BEEtat etat)
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
                tache = tacheDao.modifierTache(id, nom, detail, debut, fin, idUser, categories, etat);
                if (tache == null)
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

        public List<BETache> rechercherTache(string nom, int id)
        {
            List<BETache> retour = new List<BETache>();
            TacheDao tacheDao = new TacheDao();

            List<BETache> list = new List<BETache>();


            try
            {
                list = tacheDao.getTaches(id);
                if (list == null)
                {
                    throw new Exception("Erreur lors de la récupération des taches.");
                }

                else
                {
                    foreach (BETache be in list)
                    {
                        if (be.Nom == nom)
                        {
                            retour.Add(be);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                 throw new Exception("Une erreur est survenue lors de la récupération des taches", ex);
            }

            return retour;
        }


        public List<BETache> rechercherRetardTache(int id)
        {
            List<BETache> retour = new List<BETache>();
            TacheDao tacheDao = new TacheDao();

            List<BETache> list = new List<BETache>();


            try
            {
                list = tacheDao.getTaches(id);
                if (list == null)
                {
                    throw new Exception("Erreur lors de la récupération des taches.");
                }

                else
                {
                    foreach (BETache be in list)
                    {
                        if (DateTime.Compare(be.Fin, DateTime.Now) <= 0 && be.Etat.Libelle!="Terminée")
                        {
                            retour.Add(be);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                 throw new Exception("Une erreur est survenue lors de la récupération des taches", ex);
            }

            return retour;
        }


        public List<BETache> trieTache(int id)
        {
            CategoryDao catDao = new CategoryDao();
            List<BECategory> listCategories = catDao.GetCategories(id);
            List<BETache> retour = new List<BETache>();
            TacheDao tacheDao = new TacheDao();

            List<BETache> list = new List<BETache>();
            try
            {
                list = tacheDao.getTaches(id);
                if (list == null)
                {
                    throw new Exception("Erreur lors de la récupération des taches.");
                }

                else
                {
                    bool cond=true;
                    foreach (BECategory cat in listCategories)
                    {
                        foreach (BETache be in list)
                        {
                            foreach (BECategory c in be.Categories)
                            {
                                if (c.Id == cat.Id && cond)
                                {
                                    retour.Add(be);
                                    cond = false;
                                }
                                cond = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                 throw new Exception("Une erreur est survenue lors de la récupération des taches", ex);
            }

            return retour;
        }





        public List<BETache> VisualiserTache(int id)
        {

            TacheDao tacheDao = new TacheDao();

            List<BETache> list = new List<BETache>();


            try
            {
                list = tacheDao.getTaches(id);
                if (list == null)
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

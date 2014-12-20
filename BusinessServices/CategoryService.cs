﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.DataAccessLayer;

namespace TodoListUCBL.BusinessServices
{
    public class CategoryService
    {
        public BECategory AjouterCategory(int id, String nom, bool pardefaut)
        {
            CategoryDao categoryDao = new CategoryDao();

            //On ajoute
            BECategory category = null;
            try
            {
                category = categoryDao.AjouterCategory(id, nom, pardefaut);
                if(category == null)
                {
                    throw new Exception("La categorie est null.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de l'ajout de la catégorie",ex);
            }

            return category;
        }


        public bool SupprimerCategory(int id)
        {
            CategoryDao categoryDao = new CategoryDao();

            if(!categoryDao.CateogryExisteDeja(id))
            {
                throw new ArgumentException("La categorie n'existe pas", "id");
            }

            bool category = false;

            try
            {
                category = categoryDao.SupprimerCategory(id);
                if(category == false)
                {
                    throw new Exception("Erreur lors de la suppression de la category.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la suppression de la tache.", ex);
            }

            return category;
        }
    }
}

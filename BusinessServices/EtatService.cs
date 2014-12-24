using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.DataAccessLayer;

namespace TodoListUCBL.BusinessServices
{
    public class EtatService
    {

        public List<BEEtat> GetEtats()
        {
            EtatDao etatDao = new EtatDao();

            List<BEEtat> retour = new List<BEEtat>();

            try 
            {
                retour = etatDao.GetEtats();
            }
            catch(Exception ex)
            {
                throw new Exception("Problème lors de la récupération des états", ex);
            }

            return retour;
        }
    }
}

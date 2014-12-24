using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListUCBL.BusinessEntities;
using TodoListUCBL.EntityFramework;

namespace TodoListUCBL.DataAccessLayer
{
    public class EtatDao
    {
        public List<BEEtat> GetEtats()
        {
            using (TodoListUCBLEntities context = new TodoListUCBLEntities())
            {
                List<Etat> e = new List<Etat>();
                List<BEEtat> retour = new List<BEEtat>();
                e = context.EtatSet.ToList();
                
                foreach(Etat etat in e)
                {
                    BEEtat tmp = new BEEtat();
                    tmp.Id = etat.Id;
                    tmp.Libelle = etat.Libelle;
                    tmp.Code = etat.Code;

                    retour.Add(tmp);
                }


                return retour;
            }
        }
    }
}

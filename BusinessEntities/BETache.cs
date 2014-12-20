using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoListUCBL.BusinessEntities
{
    public class BETache
    {
        public BETache()
        {
            this.Categories = new List<BECategory>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public System.DateTime Debut { get; set; }
        public System.DateTime Fin { get; set; }
        public string Detail { get; set; }

        public BEEtat Etat { get; set; }
        public BEUtilisateur Utilisateur { get; set; }
        public List<BECategory> Categories { get; set; }

    }
}

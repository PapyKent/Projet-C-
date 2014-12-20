using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoListUCBL.BusinessEntities
{
    public class BEEtat
    {
        public BEEtat()
        {
            this.Taches = new List<BETache>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }

        public List<BETache> Taches { get; set; }

    }
}

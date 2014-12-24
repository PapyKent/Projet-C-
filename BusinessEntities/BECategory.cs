using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TodoListUCBL.BusinessEntities
{
    public class BECategory
    {
        public BECategory()
        {
            this.Taches = new List<BETache>();
        }
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool ParDefaut { get; set; }

        public BEUtilisateur Utilisateur { get; set; }
        public List<BETache> Taches { get; set;}

        public bool isSelected { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}

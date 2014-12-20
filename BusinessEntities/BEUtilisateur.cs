using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListUCBL.BusinessEntities
{
    /// <summary>
    /// Réplication dans un objet "simple" de l'objet utilisateur stocké dans la base de données
    /// </summary>
    public class BEUtilisateur
    {

        public BEUtilisateur()
        {

            this.Taches = new List<BETache>();
            this.Categories = new List<BECategory>();
        }
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public List<BETache> Taches { get; set; }
        public List<BECategory> Categories { get; set; }
    }
}

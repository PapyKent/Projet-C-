//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TodoListUCBL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.Taches = new HashSet<Tache>();
        }
    
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool ParDefaut { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual ICollection<Tache> Taches { get; set; }
    }
}
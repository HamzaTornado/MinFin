//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public int ID { get; set; }
        public int UtilisateurID { get; set; }
        public string Titre_article { get; set; }
        public Nullable<int> Description { get; set; }
        public Nullable<int> Contenu_article { get; set; }
        public string Image { get; set; }
        public string Url_video { get; set; }
        public Nullable<System.DateTime> Date_creation { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
    }
}

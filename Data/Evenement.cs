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
    
    public partial class Evenement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evenement()
        {
            this.Utilisateurs = new HashSet<Utilisateur>();
        }
    
        public int ID { get; set; }
        public string Titre_even { get; set; }
        public Nullable<int> Description { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> Date_debut { get; set; }
        public Nullable<System.DateTime> Date_fin { get; set; }
        public Nullable<bool> Statut { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}

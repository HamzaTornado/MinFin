//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinisitreFin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Utilisateur_Evenement2
    {
        public int UtilisateurID { get; set; }
        public int EvenementID { get; set; }
    
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
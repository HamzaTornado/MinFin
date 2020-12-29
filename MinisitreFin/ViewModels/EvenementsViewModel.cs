using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.ViewModels
{
    public class EvenementsViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Titre invalide")]
        [Display(Name = "Titre d'événement")]
        public string Titre_even { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        public string Image { get; set; }

        [Required]
        [Display(Name = "Date debut")]
        public DateTime Date_debut { get; set; }

        [Required]
        [Display(Name = "Date fin")]
        public DateTime Date_fin { get; set; }

        [Display(Name = "Statut")]
        public bool Statut { get; set; }
    }
}
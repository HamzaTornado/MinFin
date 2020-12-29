using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.ViewModels
{
    public class ProjectsViewModel
    {
        public int ID { get; set; }
        public int ID_Initiative { get; set; }
        [Display(Name = "Nom de Projet")]
        [Required]
        [StringLength(250)]
        public string Nom_projet { get; set; }

        [Display(Name = "Description de Projet")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Objectif de Projet")]
        [AllowHtml]
        public string Objectif_projet { get; set; }

        [Required]
        [Display(Name = "Date de début")]
        public DateTime Date_debut { get; set; }

        [Required]
        [Display(Name = "Date de fin")]
        public DateTime Date_fin { get; set; }
    }
}
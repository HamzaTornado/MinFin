using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.ViewModels
{
    public class InitiativesViewModel
    {
        public int ID { get; set; }

        public int UtilisateurID { get; set; }
        [Display(Name = "Nom d'initiatives")]
        [Required]
        [StringLength(250)]
        public string Nom_init { get; set; }

        [Display(Name = "Statut d'initiatives")]
        public bool Statu_init { get; set; }

        
        [Display(Name = "Date de début")]
        [Required]
        public DateTime Date_debu { get; set; }

        [Display(Name = "Date de fin")]
        [Required]
        public DateTime Date_fin { get; set; }

        [Display(Name = "Objectifs Generaux")]
        [AllowHtml]
        public string Objectifs_generaux { get; set; }

        [AllowHtml]
        [Display(Name = "Obgectifs Specifiques")]
        public string Obgectifs_specifiques { get; set; }

        [AllowHtml]
        [Display(Name = "Description Court")]
        public string Description_court { get; set; }

        [AllowHtml]
        [Display(Name = "Description Detaillee")]
        public string Description_detaillee { get; set; }

        [Required]
        [Display(Name = "Budget")]
        public float Budget { get; set; }

        [Display(Name = "Approbateur")]
        [Required]
        [StringLength(200)]
        public string Approbateur { get; set; }
   
        [Required]
        [StringLength(200)]
        [Display(Name = "Cofinancement")]
        public string Cofinancement { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Region")]
        public string Regions { get; set; }
    }
}
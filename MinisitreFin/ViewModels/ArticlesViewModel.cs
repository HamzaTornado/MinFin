using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinisitreFin.ViewModels
{
    public class ArticlesViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Titre d'article")]
        public string Titre_article { get; set; }

        [AllowHtml]
        [Display(Name = "Description d'article")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Contenu d'article ")]
        public string Contenu_article { get; set; }

        public string Image { get; set; }
        public string Url_video { get; set; }

        
        [Display(Name = "Date de creation")]
        public DateTime Date_creation { get; set; }

        public bool statu { get; set; }
    }
}
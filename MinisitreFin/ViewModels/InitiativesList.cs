using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.ViewModels
{
    public class InitiativesList
    {
        public int ID { get; set; }
        public string Nom_init { get; set; }
        public DateTime Date_debu { get; set; }
        public DateTime Date_fin { get; set; }
        public string Objectifs_generaux { get; set; }
        public string Obgectifs_specifiques { get; set; }
        public string Description_court { get; set; }
        public string Description_detaillee { get; set; }
        public float? Budget { get; set; }
        public string Approbateur { get; set; }
        public string Cofinancement { get; set; }
    }
}
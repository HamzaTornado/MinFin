using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.ViewModels
{
    public class EvenementsList
    {
        public int ID { get; set; }
        public string Titre_even { get; set; }
        public string Description { get; set; }
        public DateTime Date_debut { get; set; }
        public DateTime Date_fin { get; set; }
        
    }
}
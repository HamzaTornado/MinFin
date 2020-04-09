using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class GroupeModel
    {
        public int ID { get; set; }
        public int CreatedById { get; set; }
        public string Nom_groupe { get; set; }
        public DateTime? Date_createion { get; set; }
        public bool? Statut { get; set; }
        public string CreatedByIdString { get; set; }

        public int ActCount { get; set; }
    }
}
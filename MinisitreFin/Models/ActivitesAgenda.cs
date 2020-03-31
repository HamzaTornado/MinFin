using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class ActivitesAgenda
    {
        public int ID { get; set; }
        public string Nom_type { get; set; }
        public string Nom_activ { get; set; }
        public string Objectif_activ { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> statu { get; set; }
        public Nullable<int> AgendaID { get; set; }
        public int idGroupe { get; set; }
    }
}
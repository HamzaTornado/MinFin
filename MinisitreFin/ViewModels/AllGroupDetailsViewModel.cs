using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class AllGroupDetailsViewModel
    {
        public IEnumerable<Utilisateur> Utilisateur { get; set; }
        public IEnumerable<Groupe_thematiqe> Groupe_thematiqe { get; set; }
        public IEnumerable<Membre_group> Membre_group { get; set; }
    }
}
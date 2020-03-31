using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class GroupeMembersDetailsModel
    {
        public int IdU { get; set; } 
        public string nameUser { get; set; }
        public string emailUser { get; set; }
        public string  fichier { get; set; }
        public string Text { get; set; }
        public DateTime datecreation { get; set; }
        
        

    }
}
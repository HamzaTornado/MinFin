using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class GroupeNameMemnersModel
    {
        public int IDG { get; set; }
        public string CreatedById { get; set; }
        public int currentUser { get; set; }
        public string nomGroupe { get; set; }
        public int agendaExiste { get; set; }
        public int idagenda { get; set; }
        public DateTime dateGroupe { get; set; }
        public List<GroupeMembersDetailsModel> MemebersFiles { get; set; }
        public List<GroupeMembersDetailsModel> Memebers { get; set; }
    }
}
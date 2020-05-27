using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisitreFin.Models
{
    public class EventsModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string GoogleCalendarID { get; set; }
        public int AgendaId { get; set; }
        public int Type_ActiviteID { get; set; }
    }
}
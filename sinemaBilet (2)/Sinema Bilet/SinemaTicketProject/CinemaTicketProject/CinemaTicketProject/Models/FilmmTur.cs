using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicketProject.Models
{
    public class FilmmTur
    {
        public int FilmId { get; set; } 
        public int TurId { get; set; }
        public List<SelectListItem> TurList { get; set; }
    }
}
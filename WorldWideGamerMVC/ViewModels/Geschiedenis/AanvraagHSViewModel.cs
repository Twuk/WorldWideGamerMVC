using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.ViewModels.Geschiedenis
{
    public class AanvraagHSViewModel
    {
        public string UserId { get; set; }

        public List<SelectListItem> Tegenstander { get; set; }

        public Speler Winnaar { get; set; }

        public int GameId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}
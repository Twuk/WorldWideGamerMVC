using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldWideGamerMVC.ViewModels.Geschiedenis
{
    public class AanvraagAoEViewModel
    {
        public string UserId { get; set; }

        public int TeamID { get; set; }

        public int AantalSpelers { get; set; }

        public List<SelectListItem> meeGespeeldeSpelers { get; set; }

        public int GameId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}
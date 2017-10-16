using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.ViewModels.Geschiedenis
{
    public class AanvraagSpelSpelersViewModel
    {
        public string UserId { get; set; }

        public int AantalSpelers { get; set; }

        public List<GameViewModel> AlleGames { get; set; }

        public int GameId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}
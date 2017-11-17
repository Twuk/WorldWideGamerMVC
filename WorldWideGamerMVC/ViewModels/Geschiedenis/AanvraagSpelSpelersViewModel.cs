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
        public int TeamID { get; set; }
        public int GameId { get; set; }
        public string UserName { get; set; }

        public int AantalSpelers { get; set; }
        public List<SelectListItem> Tegenstander { get; set; }
        public List<SelectListItem> meeGespeeldeSpelers { get; set; }
        public string Winnaar { get; set; }
        public string SelectedTegenstander { get; set; }
        public List<GameViewModel> AlleGames { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}
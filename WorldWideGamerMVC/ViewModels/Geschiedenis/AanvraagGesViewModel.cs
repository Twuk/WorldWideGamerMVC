using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.ViewModels.Geschiedenis
{
    public class AanvraagGesViewModel
    {
        public string UserId { get; set; }

        public Speler GewonnenSpeler { get; set; }

        public List<GameViewModel> AlleGames { get; set; }

        public int GameId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}
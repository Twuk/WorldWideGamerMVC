using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class Punten
    {
        public int puntenId { get; set; }
        public int gamerId { get; set; }
        public int gameId { get; set; }
        public double inGamePunten { get; set; }
        public double bonusPunten { get; set; }
    }
}
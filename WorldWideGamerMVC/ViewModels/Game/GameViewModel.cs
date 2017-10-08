using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.ViewModels
{
    public class GameViewModel
    {
        public int gameId { get; set; }
        public string naam { get; set; }
        public string regels { get; set; }
        
        public List<SpelerUserNameGameViewModel> spelers { get; set; }
    }
}
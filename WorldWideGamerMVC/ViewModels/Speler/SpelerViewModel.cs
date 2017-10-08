using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.ViewModels
{
    public class SpelerViewModel
    {
        public string UserId { get; set; }
        public string VoorNaam { get; set; }
        public string AchterNaam { get; set; }
        public List<SpelerUserNameGameViewModel> GespeeldeGames { get; set; }
    }

    
}
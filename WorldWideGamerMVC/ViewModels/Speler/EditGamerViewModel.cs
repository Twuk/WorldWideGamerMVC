using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.ViewModels
{
    public class EditGamerViewModel
    {
        public string UserId { get; set; }
        
        [Required]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "AchterNaam")]
        public string LastName { get; set; }

        public Dictionary<GameViewModel,bool> speeltGames { get; set; }
        public List<GameViewModel> games { get; set; }
        
        [Display(Name = "UserName")]
        public List<UserNameSpel> userNamePerSpel { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.ViewModels.Game
{
    public class EditGameViewModel
    {
        [Required]
        [Display(Name = "GameId")]
        public int GameId { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Required]
        [Display(Name = "Regels")]
        public string Regels { get; set; }
    }
}
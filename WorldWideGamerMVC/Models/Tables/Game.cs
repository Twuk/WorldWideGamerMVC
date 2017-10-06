using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }
        public string Naam { get; set; }
        public string Regels { get; set; }
        public ICollection<SpelerUserNamePerGame> Spelers { get; set; }
        public bool TeamSpel { get; set; }
        public ICollection<GeschiedenisSpel> geschiedenissen { get; set; }

    }
}
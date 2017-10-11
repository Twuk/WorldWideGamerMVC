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
        public string ImageLink { get; set; }
        public ICollection<UserNameSpel> Spelers { get; set; }
        public bool TeamSpel { get; set; }
        public ICollection<GeschiedenisDetails> geschiedenissen { get; set; }

    }
}
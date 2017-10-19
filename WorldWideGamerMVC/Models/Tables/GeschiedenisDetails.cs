using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class GeschiedenisDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeschiedenisSpelersId { get; set; }
        
        [Key, ForeignKey("UserId")]
        public Speler Speler { get; set; }
        public string UserId { get; set; }

        [Key, ForeignKey("GeschiedenisSpelId")]
        public Geschiedenis Geschiedenis { get; set; }
        public int GeschiedenisSpelId { get; set; }

        [Key, ForeignKey("GameId")]
        public Game Spel { get; set; }
        public int GameId { get; set; }

        [Key, ForeignKey("TotaalPuntenSpelerId")]
        public TotaalPuntenSpeler totaalPunten { get; set; }
        public int TotaalPuntenSpelerId { get; set; }

        public int TeamId { get; set; }

        public string puntenStringVorm { get; set; }

        public double geconverteerdePunten { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class GeschiedenisSpel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeschiedenisId { get; set; }
        public int GewonnenSpeler { get; set; }
        public int GewonnenTeam { get; set; }

        [Key, ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual ResultatenAfbeelding Afbeelding { get; set; }

        public enum Status
        {
            NietBestaand,
            Ingediend,
            Geaccepteerd,
            Geweigerd,
        };

    }
}
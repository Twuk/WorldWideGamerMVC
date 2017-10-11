using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class Geschiedenis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeschiedenisId { get; set; }
        public int GewonnenSpeler { get; set; }
        public int GewonnenTeam { get; set; }
        
        public DateTime TimeStamp { get; set; }

        // Lijst met spelers, type spel, punten, teams
        public ICollection<GeschiedenisDetails> Details { get; set; }

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
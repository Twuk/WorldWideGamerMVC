using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class ResultatenAfbeelding
    {

        [ForeignKey("Geschiedenis")]
        public int ResultatenAfbeeldingId { get; set; }
        public string AfbeeldingLink { get; set; }

        public virtual GeschiedenisSpel Geschiedenis { get; set; }
    }
}
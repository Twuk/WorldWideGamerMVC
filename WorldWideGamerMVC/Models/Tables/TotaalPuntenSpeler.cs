using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class TotaalPuntenSpeler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TotaalPuntenSpelerId { get; set; }

        [Key, ForeignKey("UserId")]
        public Speler Speler { get; set; }
        public string UserId { get; set; }

        public ICollection<GeschiedenisDetails> Details { get; set; }

        public double bonuspunten
        {
            get
            {
                return this.bonuspunten;
            }
            set
            {
                this.bonuspunten = Details.GroupBy(u => u.GameId).Count() * 5;
            }
        }

        public double getTotaalPunten()
        {
           double totaalpunten = 0;
           foreach(var detail in Details)
            {
                totaalpunten += detail.geconverteerdePunten;
            }
            return totaalpunten;
        }

        //Dit wordt een fucking moeilijke methode want we moeten kijken afhankelijk van spel convertie gaan doen
        public double getTotaalPuntenSpel(int spelId)
        {
            switch (spelId) {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;

            }
            double totaalpuntenSpel = 0;
            foreach (var detail in Details.Where(d => d.GameId == spelId))
            {
                totaalpuntenSpel += detail.geconverteerdePunten;
            }
            return totaalpuntenSpel;
        }
    }
}
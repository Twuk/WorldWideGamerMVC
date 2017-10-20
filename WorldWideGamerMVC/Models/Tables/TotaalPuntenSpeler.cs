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

        public const double TOTAALPUNTEN = 20;

        public double Bonuspunten
        {
            get
            {
                return this.Bonuspunten;
            }
            set
            {
                this.Bonuspunten = Details.GroupBy(u => u.GameId).Count() * 5;
            }
        }

        //Dit wordt een fucking moeilijke methode want we moeten kijken afhankelijk van spel convertie gaan doen
        public double GetTotaalPuntenSpel(int spelId)
        {
            double totaalpuntenSpel = 0;
            switch (spelId) {
                case 1:
                   
                    break;
                case 2:
                    
                    break;
                case 3:
                    totaalpuntenSpel = HearthStoneConvertie();
                    break;
                case 4:
                    break;
                case 5:
                    break;

            }
            return totaalpuntenSpel;
        }

        public double HearthStoneConvertie()
        {
            //20/Totaal gespeelde spellen van alles speler * aantal gewonnen + totaal gespeelde spellen
            IEnumerable<GeschiedenisDetails> gespeeldeGamesHS = Details.Where(u => u.GameId == 3);
            int totaalGespeeldeGames = gespeeldeGamesHS.Count()/2;
            int aantalGewonnen = gespeeldeGamesHS.Where(u => u.UserId == UserId).Where(u => u.Geschiedenis.GewonnenSpeler == UserId).Count();
            int zelfGespeeldeGames = gespeeldeGamesHS.Where(u => u.UserId == UserId).Count();
            return (TOTAALPUNTEN / totaalGespeeldeGames * aantalGewonnen) + zelfGespeeldeGames;
        }
    }
}
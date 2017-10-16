namespace WorldWideGamerMVC.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WorldWideGamerMVC.Models;
    using WorldWideGamerMVC.Models.Tables;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<WorldWideGamerMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WorldWideGamerMVC.Models.ApplicationDbContext";
        }

        protected override void Seed(WorldWideGamerMVC.Models.ApplicationDbContext context)
        {

            /*
        var store = new UserStore<ApplicationUser>(context);
        var manager = new ApplicationUserManager(store);
        var speler = new Speler { FirstName = "Tom", LastName = "Dorchain" };
        var user = new ApplicationUser() { Email = "tdorchain@gmail.com", UserName = "tdorchain@gmail.com", speler = speler };
        manager.Create(user, "Demiete6");
        var result = manager.AddToRole(user.Id, "Admin");

        var speler2 = new Speler { FirstName = "Joriek", LastName = "Rogge" };
        var user2 = new ApplicationUser() { Email = "joriek_rogge@gmail.com", UserName = "joriek_rogge@gmail.com", speler = speler2 };
        manager.Create(user2, "Demiete6");

            var spel = new Game() { Naam = "Age of Empires 2", Regels = "Regels voor Age of Empires 2", TeamSpel = true, ImageLink = "../../Images/Age_of_Empires_II.jpg" };
            context.Games.Add(spel);
            context.SaveChanges();
            var spel2 = new Game() { Naam = "CounterStrike", Regels = "Regels voor CounterStrike", TeamSpel = true, ImageLink = "../../Images/counter_strike.jpg" };
            context.Games.Add(spel2);
            context.SaveChanges();

            var spel3 = new Game() { Naam = "HeartStone", Regels = "Regels voor HeartStone", TeamSpel = false, ImageLink = "../../Images/hearthstone.png" };
            context.Games.Add(spel3);
            context.SaveChanges();

            var spel4 = new Game() { Naam = "League of Legends", Regels = "Regels voor League of Legends", TeamSpel = true, ImageLink = "../../Images/League_of_Legends.jpg" };
            context.Games.Add(spel4);
            context.SaveChanges();

            var spel5 = new Game() { Naam = "Fifa", Regels = "Regels voor Fifa", TeamSpel = false, ImageLink = "" };
            context.Games.Add(spel5);
            context.SaveChanges();

   */
        }
    }
}

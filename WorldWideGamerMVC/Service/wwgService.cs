using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace WorldWideGamerMVC.Service
{
    public class wwgService
    {

        public IEnumerable<object> Roles { get; internal set; }
        public ApplicationDbContext _ApplicationDb;

        public wwgService()
        {
            this._ApplicationDb = new ApplicationDbContext();
        }

        public wwgService(ApplicationDbContext applicationDb)
        {
            this._ApplicationDb = applicationDb;
        }
        


        #region gamer
        public List<Speler> GetGamers()
        {
            List<Speler> spelers = new List<Speler>();
            foreach(var speler in _ApplicationDb.Spelers)
            {
                spelers.Add(speler);
            }
            return spelers;
        }

        
        public Speler GetGamer(string gamerId)
        {
            var query = from p in _ApplicationDb.Spelers
                        where p.UserId == gamerId
                        select p;
            var speler = query.Single();

            return speler;
        }

        public void PostGamer(Speler gamer)
        {
            
            var original = _ApplicationDb.Spelers.Find(gamer.UserId);

            if (original != null)
            {
                original.FirstName = gamer.FirstName;
                original.LastName = gamer.LastName;
                //Removed eerst alles en voegt dan terug toe
                //Heb nog te weinig kennis van EF om met niet unique foreign keys update te doen
                var speeltGames = _ApplicationDb.SpelerGames.Where(u => u.UserId == original.UserId);
                foreach(SpelerUserNamePerGame obj in speeltGames)
                {
                    _ApplicationDb.SpelerGames.Remove(obj);
                }
                original.SpeeltGames = gamer.SpeeltGames;
                _ApplicationDb.SaveChanges();
            }
            else
            {
                throw new Exception("User bestaat niet");
            }
        }


        public void DeleteGamer(int id)
        {
            /*
            if (this.OpenConnection() == true)
            {
                MySqlCommand comm = connection.CreateCommand();
                comm.CommandText = "DELETE FROM gamers where gamerId = @gamerId";
                comm.Parameters.Add("@gamerId", id);
                comm.ExecuteNonQuery();

                comm.CommandText = "DELETE FROM gamers_games where gamerId = @gamerIdgames";
                comm.Parameters.Add("@gamerIdgames", id);
                comm.ExecuteNonQuery();

                connection.Close();
            }
            */
        }
        #endregion

        #region game
        public List<Game> GetGames()
        {
            
            List<Game> games = new List<Game>();
            List<SpelerUserNamePerGame> allUserNames = new List<SpelerUserNamePerGame>();
            List<SpelerUserNamePerGame> userNames = new List<SpelerUserNamePerGame>();
            foreach (var userName in _ApplicationDb.SpelerGames)
            {
                allUserNames.Add(userName);
            }

            foreach (var game in _ApplicationDb.Games)
            {
                userNames = new List<SpelerUserNamePerGame>();
                foreach (var userNameGame in allUserNames.Where(u => u.GameId == game.GameId))
                {
                    
                    userNames.Add(userNameGame);
                }
                game.Spelers = userNames;
                games.Add(game);
            }
            
            return games;
        }

        public Game GetGame(int gameId)
        {

            var spel = _ApplicationDb.Games.First(g => g.GameId == gameId);
            return spel;
        }

        public void SetSpelRegels(int gameId, string regels)
        {
            var original = _ApplicationDb.Games.Find(gameId);

            if (original != null)
            {
                original.Regels = regels;
                _ApplicationDb.SaveChanges();
            }
            else
            {
                throw new Exception("Spel bestaat niet");
            }
        }



        #endregion
    }
}
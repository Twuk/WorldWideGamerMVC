using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Service;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.Models.BusinessLayer
{
    public class GamerBusinessLayer
    {
        public wwgService databaseConnectionClass;
        public GamerBusinessLayer(ApplicationDbContext wwgService)
        {
            databaseConnectionClass = new wwgService(wwgService);
        }

        public GamerBusinessLayer()
        {
            databaseConnectionClass = new wwgService();
        }

        #region GET
        public List<Speler> GetGamers()
        {
            return databaseConnectionClass.GetGamers();
        }

        public Speler GetGamer(string gamerId)
        {
            return databaseConnectionClass.GetGamer(gamerId);
        }

        public List<Game> GetGamerGames(string userId)
        {
            return databaseConnectionClass.GetGamerGames(userId);
        }
        
        #endregion

        #region POST
        public void PostGamer(Speler gamer)
        {
            databaseConnectionClass.PostGamer(gamer);
        }
        #endregion

        #region DELETE
        public void DeleteGamer(int id)
        {
            databaseConnectionClass.DeleteGamer(id);
        }

        #endregion
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Service;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.Models.BusinessLayer
{
    public class GameBusinessLayer
    {
        public wwgService databaseConnectionClass;

        public GameBusinessLayer(ApplicationDbContext wwgService)
        {
            databaseConnectionClass = new wwgService(wwgService) ;
        }

        public GameBusinessLayer()
        {
            databaseConnectionClass = new wwgService();
        }

        public Game getGame(int gameId)
        {
            return databaseConnectionClass.GetGame(gameId); ;
        }

        public List<Game> getGames()
        {
            return databaseConnectionClass.GetGames(); ;
        }

        public void setSpelRegels(int gameId, string regels)
        {
            databaseConnectionClass.SetSpelRegels(gameId, regels);
        }
    }
}
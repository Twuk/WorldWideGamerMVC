using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Service;
using WorldWideGamerMVC.Models.Tables;

namespace WorldWideGamerMVC.Models.BusinessLayer
{
    public class GeschiedenisBusinessLayer
    {
        public wwgService databaseConnectionClass;
        public GeschiedenisBusinessLayer(ApplicationDbContext wwgService)
        {
            databaseConnectionClass = new wwgService(wwgService);
        }

        public GeschiedenisBusinessLayer()
        {
            databaseConnectionClass = new wwgService();
        }

        #region POST
        public void PostHeartStoneSpel(Speler gamer)
        {
            databaseConnectionClass.PostGamer(gamer);
        }
    }
}
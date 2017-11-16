using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideGamerMVC.Models.BusinessLayer;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WorldWideGamerMVC.Models;
using WorldWideGamerMVC.Models.HulpMethods;
using WorldWideGamerMVC.ViewModels.Game;

namespace WorldWideGamerMVC.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;
        private FillViewsMethods fillView;

        public GameController()
        {
            databaseConnectie = new ApplicationDbContext();
            gameBal = new GameBusinessLayer(databaseConnectie);
            gamerBal = new GamerBusinessLayer(databaseConnectie);
            fillView = new FillViewsMethods(gameBal, gamerBal);
        }

        public ActionResult GamesOverzicht()
        {
            List<GameViewModel> gameModels = new List<GameViewModel>();
            GamesViewModel spelenOverzicht = new GamesViewModel();
            List<Game> games = gameBal.getGames();
            foreach(Game spel in games)
            {
                GameViewModel gameView = fillView.FillGameViewModel(spel);
                foreach(UserNameSpel user in spel.Spelers){
                    SpelerUserNameGameViewModel userNameView = fillView.FillUserNameViewModel(user);
                    gameView.spelers.Add(userNameView);
                }
                gameModels.Add(gameView);
            }
            spelenOverzicht.Games = gameModels;
            return View("GamesOverzicht",spelenOverzicht);
        }

        public ActionResult DetailGame(int gameId)
        {
            Game spel;
           
               spel = gameBal.getGame(gameId);
                if (spel != null)
                {
                    GameViewModel gameView = fillView.FillGameViewModel(spel);
                    foreach (UserNameSpel user in spel.Spelers)
                    {
                        SpelerUserNameGameViewModel userNameView = fillView.FillUserNameViewModel(user);
                        gameView.spelers.Add(userNameView);
                    }
                    return View("DetailGame", gameView);
                }
           
            return GamesOverzicht() ;
        }

        [Authorize]
        public ActionResult EditGame(int gameId)
        {
            EditGameViewModel editGameView = new EditGameViewModel();
            Game spel = gameBal.getGame(gameId);
            if(spel == null)
            {
                return View("Index", "Home");
            }
            editGameView.GameId = spel.GameId;
            editGameView.Naam = spel.Naam;
            editGameView.Regels = spel.Regels;
            return View(editGameView);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveGame(EditGameViewModel editGameView)
        {
            gameBal.setSpelRegels( editGameView.GameId, editGameView.Regels);
            return GamesOverzicht();
        }


    }
}
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

namespace WorldWideGamerMVC.Controllers
{
    public class SpelerController : Controller
    {

        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;
        private FillViewsMethods fillView;

        public SpelerController()
        {
            databaseConnectie = new ApplicationDbContext();
            gameBal = new GameBusinessLayer(databaseConnectie);
            gamerBal = new GamerBusinessLayer(databaseConnectie);
            fillView = new FillViewsMethods(gameBal, gamerBal);
        }

        public ActionResult GamersOverzicht()
        {
            SpelersViewModel gamersViewModel = new SpelersViewModel();
            List<Speler> gamers = gamerBal.GetGamers();
            List<SpelerViewModel> listGamerViewModels = new List<SpelerViewModel>();
            foreach (Speler speler in gamers)
            {
                SpelerViewModel gamerViewModel = new SpelerViewModel();
                List<SpelerUserNameGameViewModel> userNamesGameViewModels = new List<SpelerUserNameGameViewModel>();
                gamerViewModel.UserId = speler.UserId;
                gamerViewModel.VoorNaam = speler.FirstName;
                gamerViewModel.AchterNaam = speler.LastName;
                foreach (UserNameSpel obj in speler.SpeeltGames)
                {
                    SpelerUserNameGameViewModel spelerUserNameGame = new SpelerUserNameGameViewModel();
                    spelerUserNameGame.GameViewModel = fillView.FillGameViewModel(gameBal.getGame(obj.GameId));
                    spelerUserNameGame.Username = obj.userName;
                    userNamesGameViewModels.Add(spelerUserNameGame);
                }
                gamerViewModel.GespeeldeGames = userNamesGameViewModels;
                listGamerViewModels.Add(gamerViewModel);
            }
            gamersViewModel.gamers = listGamerViewModels;
            return View("GamersOverzicht", gamersViewModel);
        }

        public ActionResult EditGamer(string id)
        {
            EditGamerViewModel edit = new EditGamerViewModel();
            Dictionary<GameViewModel, bool> speeltGamesDict = new Dictionary<GameViewModel, bool>();
            List<GameViewModel> games = new List<GameViewModel>();
            if (id == null)
            {
                id = User.Identity.GetUserId();
            }
            var speler = gamerBal.GetGamer(id);
            if (speler != null)
            {
                edit.UserId = speler.UserId;
                edit.FirstName = speler.FirstName;
                edit.LastName = speler.LastName;
                List<UserNameSpel> list = new List<UserNameSpel>();
                foreach (Game spel in gameBal.getGames())
                {
                    GameViewModel gameView = fillView.FillGameViewModel(spel);
                    UserNameSpel obj;
                    games.Add(gameView);
                    if (!speler.SpeeltGames.Any(u => u.GameId == spel.GameId))
                    {
                        speeltGamesDict.Add(gameView, false);
                        obj = new UserNameSpel();
                        obj.GameId = spel.GameId;
                        obj.userName = "";
                    }
                    else
                    {
                        speeltGamesDict.Add(gameView, true);
                        obj = speler.SpeeltGames.Where(u => u.GameId == spel.GameId).First();
                    }
                    list.Add(obj);
                }
                edit.userNamePerSpel = list;
                edit.games = games;
                edit.speeltGames = speeltGamesDict;
                return View("EditGamer", edit);
            }
            else
            {
                return GamersOverzicht();
            }
        }

        public ActionResult DeleteGamer(int id)
        {
            gamerBal.DeleteGamer(id);
            return GamersOverzicht();
        }

        [HttpPost]
        public ActionResult SaveGamer(EditGamerViewModel e, List<int> gameSelecter)
        {
            if (ModelState.IsValid)
            {
                Speler gamer = new Speler();
                gamer.UserId = e.UserId;
                gamer.FirstName = e.FirstName;
                gamer.LastName = e.LastName;
                var store = new UserStore<ApplicationUser>(databaseConnectie);
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindById(e.UserId);
                gamer.User = user;
                List<UserNameSpel> userNames = new List<UserNameSpel>();
                foreach (int key in gameSelecter)
                {
                    userNames.Add(e.userNamePerSpel.Where(l => l.GameId == key).First());
                }
                gamer.SpeeltGames = userNames;
                gamerBal.PostGamer(gamer);
                return GamersOverzicht();
            }

            else
            {
                ModelState.AddModelError("CredentialError", "Je hebt iets leeg gelaten");
            }
            return EditGamer(e.UserId);
        }
    }
}
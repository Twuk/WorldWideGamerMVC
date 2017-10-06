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

namespace WorldWideGamerMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;

        public HomeController()
        {
            databaseConnectie = new ApplicationDbContext();
            gameBal = new GameBusinessLayer(databaseConnectie);
            gamerBal = new GamerBusinessLayer(databaseConnectie);
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                

                gamerViewModel.VoorNaam = speler.FirstName;
                gamerViewModel.AchterNaam = speler.LastName;
                
                foreach (SpelerUserNamePerGame obj in speler.SpeeltGames)
                {
                    SpelerUserNameGameViewModel spelerUserNameGame = new SpelerUserNameGameViewModel();
                    spelerUserNameGame.GameViewModel = FillGameViewModel(gameBal.getGame(obj.GameId));
                    spelerUserNameGame.Username = obj.userName;
                    userNamesGameViewModels.Add(spelerUserNameGame);
                }
                gamerViewModel.GespeeldeGames = userNamesGameViewModels;

                listGamerViewModels.Add(gamerViewModel);
            }
            gamersViewModel.gamers = listGamerViewModels;

            return View("GamersOverzicht", gamersViewModel);
        }

        public ActionResult editGamer(string id)
        {

            EditGamerViewModel edit = new EditGamerViewModel();

            List<GameViewModel> games = new List<GameViewModel>();
            if (id == null)
            {
                id = User.Identity.GetUserId();
            }
            var speler = gamerBal.GetGamer(id);
            if(speler != null)
            {
                edit.FirstName = speler.FirstName;
                edit.LastName = speler.LastName;
                List<SpelerUserNamePerGame> list = new List<SpelerUserNamePerGame>() ;
                foreach (Game spel in gameBal.getGames())
                {
                    games.Add(FillGameViewModel(spel));
                    SpelerUserNamePerGame obj;
                    if (!speler.SpeeltGames.Any(u => u.GameId == spel.GameId))
                    {
                        obj = new SpelerUserNamePerGame();
                        obj.GameId = spel.GameId;
                        obj.userName = "";
                    }
                    else
                    {
                        obj = speler.SpeeltGames.Where(u => u.GameId == spel.GameId).First();
                    }
                    list.Add(obj);
                }
                edit.userNamePerSpel = list;

                edit.games = games;
                return View("EditGamer", edit);

            }
            else
            {
                return GamersOverzicht();
            }
            
            
            return GamersOverzicht();
        }

        public ActionResult deleteGamer(int id)
        {
            gamerBal.DeleteGamer(id);
            return GamersOverzicht();
        }

        [HttpPost]
        public ActionResult SaveGamer(EditGamerViewModel e, List<int> gameSelecter)
        {
            /*
              if (ModelState.IsValid)
              {
                  Speler gamer = new Speler();
                  gamer.UserId = e.gamerViewModel.UserId;
                  gamer.FirstName = e.gamerViewModel.VoorNaam;
                  gamer.LastName = e.gamerViewModel.AchterNaam;
                  var store = new UserStore<ApplicationUser>(databaseConnectie);
                  var userManager = new UserManager<ApplicationUser>(store);
                  ApplicationUser user = userManager.FindById(e.gamerViewModel.UserId);
                  gamer.User = user;
                  List<Game> games = new List<Game>();
                  foreach (int Gameid in gameSelecter)
                  {
                      Game game = gameBal.getGame(Gameid);
                      games.Add(game);
                  }
                  //gamer.SpeeltGames = null;
                  gamerBal.PostGamer(gamer);

                  return GamersOverzicht();
              }

              else
              {
                  //Hier nog errorvalidation mss
                  ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                  return GamersOverzicht();
              } */
            return GamersOverzicht();
        }

        public GameViewModel FillGameViewModel(Game game)
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.gameId = game.GameId;
            gameViewModel.naam = game.Naam;
            gameViewModel.regels = game.Regels;
            return gameViewModel;
        }

        public SpelerViewModel FillSpelerViewModel(Speler speler)
        {
            SpelerViewModel spelerViewModel = new SpelerViewModel();
            spelerViewModel.VoorNaam = speler.FirstName;
            spelerViewModel.AchterNaam = speler.LastName;
            return spelerViewModel;
        }

        public SpelerUserNameGameViewModel FillUserNameViewModel(SpelerUserNamePerGame username)
        {
            SpelerUserNameGameViewModel userNameViewModel = new SpelerUserNameGameViewModel();
            if(username.speler != null)
            {
                userNameViewModel.GameViewModel = FillGameViewModel(gameBal.getGame(username.GameId));
                userNameViewModel.Username = username.userName;
            }
            else if(username.Game != null)
            {

                userNameViewModel.SpelerViewModel = FillSpelerViewModel(gamerBal.GetGamer(username.UserId));
                userNameViewModel.Username = username.userName;
            }
            else
            {
                userNameViewModel.GameViewModel = FillGameViewModel(gameBal.getGame(username.GameId));
                userNameViewModel.Username = username.userName;
                userNameViewModel.SpelerViewModel = FillSpelerViewModel(gamerBal.GetGamer(username.UserId));
            }
            return userNameViewModel;
        }
    }
}
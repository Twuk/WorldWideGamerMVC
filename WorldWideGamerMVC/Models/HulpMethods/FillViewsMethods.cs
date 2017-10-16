using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldWideGamerMVC.Models.BusinessLayer;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.ViewModels;

namespace WorldWideGamerMVC.Models.HulpMethods
{
    
    public class FillViewsMethods
    {
        private GameBusinessLayer gameBalCon { get; set; }
        private GamerBusinessLayer gamerBalCon { get; set; }

        public FillViewsMethods(GameBusinessLayer gameBal, GamerBusinessLayer gamerBal)
        {
            gameBalCon = gameBal;
            gamerBalCon = gamerBal;
        }

        public GameViewModel FillGameViewModel(Game game)
        {
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.gameId = game.GameId;
            gameViewModel.naam = game.Naam;
            gameViewModel.regels = game.Regels;
            gameViewModel.ImageLink = game.ImageLink;
            gameViewModel.spelers = new List<SpelerUserNameGameViewModel>();
            return gameViewModel;
        }

        public SpelerViewModel FillSpelerViewModel(Speler speler)
        {
            SpelerViewModel spelerViewModel = new SpelerViewModel();
            spelerViewModel.VoorNaam = speler.FirstName;
            spelerViewModel.AchterNaam = speler.LastName;
            spelerViewModel.GespeeldeGames = new List<SpelerUserNameGameViewModel>();
            return spelerViewModel;
        }

        public SpelerUserNameGameViewModel FillUserNameViewModel(UserNameSpel username)
        {
            SpelerUserNameGameViewModel userNameViewModel = new SpelerUserNameGameViewModel();
            if (username.speler != null && username.Game != null)
            {
                userNameViewModel.GameViewModel = FillGameViewModel(gameBalCon.getGame(username.GameId));
                userNameViewModel.Username = username.UserName;
                userNameViewModel.SpelerViewModel = FillSpelerViewModel(gamerBalCon.GetGamer(username.UserId));
            }
            else if (username.speler != null && username.Game == null)
            {
                userNameViewModel.GameViewModel = FillGameViewModel(gameBalCon.getGame(username.GameId));
                userNameViewModel.Username = username.UserName;
            }
            else if (username.Game != null && username.speler == null)
            {

                userNameViewModel.SpelerViewModel = FillSpelerViewModel(gamerBalCon.GetGamer(username.UserId));
                userNameViewModel.Username = username.UserName;
            }
            else
            {
                userNameViewModel.Username = username.UserName;
            }
            return userNameViewModel;
        }
    }
}
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
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;

        public FillViewsMethods(GameBusinessLayer gameBal, GamerBusinessLayer gamerBal)
        {
            gameBal = gameBal;
            gamerBal = gamerBal;
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
            if (username.speler != null)
            {
                userNameViewModel.GameViewModel = FillGameViewModel(gameBal.getGame(username.GameId));
                userNameViewModel.Username = username.userName;
            }
            else if (username.Game != null)
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
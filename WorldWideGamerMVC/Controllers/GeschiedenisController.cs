using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideGamerMVC.Models;
using WorldWideGamerMVC.Models.BusinessLayer;
using WorldWideGamerMVC.Models.HulpMethods;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.ViewModels;
using WorldWideGamerMVC.ViewModels.Geschiedenis;

namespace WorldWideGamerMVC.Controllers
{
    public class GeschiedenisController : Controller
    {
        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;
        private FillViewsMethods fillView;

        public GeschiedenisController()
        {
            databaseConnectie = new ApplicationDbContext();
            gameBal = new GameBusinessLayer(databaseConnectie);
            gamerBal = new GamerBusinessLayer(databaseConnectie);
            fillView = new FillViewsMethods(gameBal, gamerBal);
        }
        
        public ActionResult Aanvraag()
        {
            string userId = User.Identity.GetUserId();
            AanvraagSpelSpelersViewModel aanvraag = new AanvraagSpelSpelersViewModel();
            List<GameViewModel> gameViewModels = new List<GameViewModel>();
            foreach(var spel in gameBal.getGames())
            {
                gameViewModels.Add(fillView.FillGameViewModel(spel));
            }
            aanvraag.AlleGames = gameViewModels;
            aanvraag.UserId = userId;
            return View("AanvraagSpelSpelers",aanvraag);
        }
        public bool isTeamSpel(int gameId)
        {
            bool isTeam = gameBal.getGame(gameId).TeamSpel;
            return isTeam;
        }

        [HttpPost]
        public ActionResult Aanvraag(AanvraagSpelSpelersViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.GameId)
                {
                    case 1:
                        return AanvraagAoe(model);
                    case 2:
                        break;
                    case 3:
                        return AanvraagHS(model);
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
            ModelState.AddModelError("",new Exception("Je hebt een foutje gemaakt"));
            return View();
        }

        public ActionResult AanvraagAoe(AanvraagSpelSpelersViewModel model)
        {
            AanvraagAoEViewModel volgendeAanvraag = new AanvraagAoEViewModel();
            Speler ingediende = gamerBal.GetGamer(model.UserId);
            model.UserName = ingediende.SpeeltGames.Where(u => u.GameId == model.GameId).First().UserName;
            volgendeAanvraag.AantalSpelers = model.AantalSpelers;
            volgendeAanvraag.GameId = model.GameId;
            Game spel = new Game();
            spel = gameBal.getGame(model.GameId);
            if (GenoegGeregistreerdeSpelers(model.AantalSpelers, spel))
            {
                List<Speler> spelers = gamerBal.GetGamers();
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var speler in spelers)
                {
                    if (spel.Spelers.Any(u => u.UserId == speler.UserId))
                    {
                        UserNameSpel userNameSpel = spel.Spelers.Where(u => u.UserId == speler.UserId).First();
                        items.Add(new SelectListItem { Text = userNameSpel.UserName, Value = userNameSpel.UserId });
                    }

                }
                volgendeAanvraag.meeGespeeldeSpelers = items;
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    saveImage(model.Image, spel.Naam);
                    volgendeAanvraag.Image = model.Image;
                }

            }
            else
            {
                string errorMessage = "Er zijn niet zoveel spelers geregistreerd voor " + spel.Naam;
                return View("Error", new ErrorViewModel(errorMessage));
            }
            return View("AanvraagAoE", volgendeAanvraag);
        }

        public ActionResult AanvraagHS(AanvraagSpelSpelersViewModel model)
        {
            AanvraagSpelSpelersViewModel volgendeAanvraag = new AanvraagSpelSpelersViewModel();

            Speler ingediende = gamerBal.GetGamer(model.UserId);
            volgendeAanvraag.UserName = ingediende.SpeeltGames.Where(u => u.GameId == model.GameId).First().UserName;
            volgendeAanvraag.GameId = model.GameId;
            Game spel = new Game();
            spel = gameBal.getGame(model.GameId);
            if (GenoegGeregistreerdeSpelers(model.AantalSpelers, spel))
            {
                List<Speler> spelers = gamerBal.GetGamers();
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var speler in spelers)
                {
                    if (spel.Spelers.Any(u => u.UserId == speler.UserId))
                    {
                        UserNameSpel userNameSpel = spel.Spelers.Where(u => u.UserId == speler.UserId).First();
                        items.Add(new SelectListItem { Text = userNameSpel.UserName, Value = userNameSpel.UserId });
                    }

                }
                volgendeAanvraag.Tegenstander = items;
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    saveImage(model.Image, spel.Naam);
                    volgendeAanvraag.Image = model.Image;
                }

            }
            else
            {
                string errorMessage = "Er zijn niet zoveel spelers geregistreerd voor " + spel.Naam;
                return View("Error", new ErrorViewModel(errorMessage));
            }
            return View("AanvraagHS", volgendeAanvraag);
        }

        public void saveImage (HttpPostedFileBase image, string spelnaam)
        {
            string displayName = image.FileName;
            string fileExtension = Path.GetExtension(displayName);
            var date = DateTime.Now;
            string tijdConvertie = date.ToString("yyMMddHmmss");
            string fileName = string.Format("{0}{1}", tijdConvertie, fileExtension);
            string uploadPath = "~/Images/Geschiedenis/" + spelnaam + "/";
            string path = Path.Combine(Server.MapPath(uploadPath), fileName);
            image.SaveAs(path);
        }

        public bool GenoegGeregistreerdeSpelers(int aantalSpelers, Game spel)
        {
            if(spel.Spelers.Count >= aantalSpelers)
            {
                return true;
            }
            return false;
        }
    }
}
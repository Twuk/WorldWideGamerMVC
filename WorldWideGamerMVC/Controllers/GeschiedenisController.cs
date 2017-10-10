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
            AanvraagGesViewModel aanvraag = new AanvraagGesViewModel();
            List<GameViewModel> gameViewModels = new List<GameViewModel>();
            foreach(var spel in gameBal.getGames())
            {
                gameViewModels.Add(fillView.FillGameViewModel(spel));
            }
            aanvraag.AlleGames = gameViewModels;
            aanvraag.UserId = userId;
            return View("AanvraagGes",aanvraag);
        }

        [HttpPost]
        public ActionResult Aanvraag(AanvraagGesViewModel model, int gameSelecter)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                AanvraagGesViewModel aanvraag = new AanvraagGesViewModel();
                aanvraag.UserId = userId;
                if (model.Image != null && model.Image.ContentLength > 0)
                {
                    string displayName = model.Image.FileName;
                    string fileExtension = Path.GetExtension(displayName);
                    var date = DateTime.Now;
                    string tijdConvertie = date.ToString("yy/MM/dd H:mm:ss");
                    string fileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    model.Image.SaveAs(path);
                    // Update data model
                }
            }
            return View();
        }

    }
}
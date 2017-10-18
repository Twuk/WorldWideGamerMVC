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
using System.Web.Security;
using System.Security.Claims;

namespace WorldWideGamerMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;
        private FillViewsMethods fillView;
        private Random rnd = new Random();

        public HomeController()
        {
            databaseConnectie = new ApplicationDbContext();
            gameBal = new GameBusinessLayer(databaseConnectie);
            gamerBal = new GamerBusinessLayer(databaseConnectie);
            fillView = new FillViewsMethods(gameBal, gamerBal);
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var store = new UserStore<ApplicationUser>(databaseConnectie);
            var manager = new UserManager<ApplicationUser>(store);
            var user = databaseConnectie.Users.Where(u => u.Email == "tdorchain@gmail.com").First();
            manager.AddToRole(user.Id, "Admin");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Beheer()
        {
            return View();
        }


    }
}
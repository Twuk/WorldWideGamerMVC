﻿using System;
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
    public class HomeController : Controller
    {
        private ApplicationDbContext databaseConnectie;
        private GameBusinessLayer gameBal;
        private GamerBusinessLayer gamerBal;
        private FillViewsMethods fillView;

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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
      
    }
}
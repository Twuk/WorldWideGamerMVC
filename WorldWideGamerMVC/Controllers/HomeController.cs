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
using System.IO;
using WorldWideGamerMVC.ViewModels.Gamer;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

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

        public async System.Threading.Tasks.Task<ActionResult> BeheerAsync()
        {
            try
            {
                List<List<string>> lijsten = loadExcelNames();
                List<string> voornamen = lijsten[0];
                List<string> achternamen = lijsten[1];

                int aantal_spelers = 0;
                Console.WriteLine("Geef het aantal spelers in die je wilt genereren : \n");
                aantal_spelers = 50;

                for (int i = 0; i < aantal_spelers; i++)
                {
                    int voornaamPlaats = rnd.Next(0, voornamen.Count + 1);
                    int achternaamPlaats = rnd.Next(0, achternamen.Count + 1);
                    string voornaam = voornamen[voornaamPlaats].Trim();
                    string achternaam = achternamen[achternaamPlaats].Trim();
                    List<int> gameIds = new List<int>();
                    RegisterGamerViewModel model = new RegisterGamerViewModel();
                    List<UserNameSpel> userNames = new List<UserNameSpel>();
                    //random generator tussen 1-5 die usernameperSpel object aanmaakt en in lijst steekt
                    int aantal_games = rnd.Next(1, 6);
                    for (int j = 0; j < aantal_games; j++)
                    {
                        int number;
                        do
                        {
                            number = rnd.Next(1, 6);
                        } while (gameIds.Contains(number));
                        gameIds.Add(number);
                        string usernaam = achternaam + "_" + voornaam + j.ToString();
                        UserNameSpel userName = new UserNameSpel(number, usernaam);
                        userNames.Add(userName);
                    }
                    model.FirstName = voornaam;
                    model.LastName = achternaam;
                    model.Email = voornaam + "_" + achternaam + "@gmail.com";
                    model.Password = voornaam + "_" + achternaam + "6";
                    model.userNamePerSpel = userNames;
                    var result = await RegisterSpelersAsync(model, gameIds);
                }
            }
            catch (System.Exception ex)
            {
                return View("Error",new ErrorViewModel(ex.ToString()));
            }
            return View("Index");
        }
        public async Task<IdentityResult> RegisterSpelersAsync(RegisterGamerViewModel model, List<int> gameselecter)
        {
            List<UserNameSpel> userNames = new List<UserNameSpel>();
            foreach (int key in gameselecter)
            {
                userNames.Add(model.userNamePerSpel.Where(l => l.GameId == key).First());
            }

            var speler = new Speler { FirstName = model.FirstName, LastName = model.LastName, SpeeltGames = userNames };
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, speler = speler };
            var result = await Request.GetOwinContext().GetUserManager<ApplicationUserManager>().CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await HttpContext.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return result;
            }
            return result;
        }
        public List<List<string>> loadExcelNames()
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            using (var reader = new StreamReader(@"C:\Users\tom_d\Documents\GitHub\WorldWideGamerMVC\FillDatabaseTest\NamenLijst.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }
            List<List<string>> lijsten = new List<List<string>>();
            lijsten.Add(listA);
            lijsten.Add(listB);
            return lijsten;
        }
    

}
}
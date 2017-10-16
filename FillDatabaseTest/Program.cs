using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldWideGamerMVC.Controllers;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.ViewModels.Gamer;

namespace FillDatabaseTest
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            try
            {
                int aantal_spelers = 0;
                Console.WriteLine("Geef het aantal spelers in die je wilt genereren : \n");
                aantal_spelers = Convert.ToInt16(Console.ReadLine());
                
                for (int i = 0; i < aantal_spelers; i++)
                {
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
                        UserNameSpel userName = new UserNameSpel(number, "");
                    }
                    Console.WriteLine("end speler {0}\n",i);
                }
                System.Threading.Thread.Sleep(15000);
                /* FirstName = model.FirstName, LastName = model.LastName, SpeeltGames = userNames
                     UserName = model.Email, Email = model.Email
                     model.Password
                     */
                /*
                List<string> voorNamen = new List<string>();
                List<string> achterNamen = new List<string>();
                var result = new WorldWideGamerMVC.Controllers.AccountController().Register(model, gameSelecter);
               */
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}

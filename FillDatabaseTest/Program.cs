using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldWideGamerMVC.Controllers;
using WorldWideGamerMVC.Models.Tables;
using WorldWideGamerMVC.ViewModels.Gamer;
using Microsoft.Office.Interop;
using System.IO;

namespace FillDatabaseTest
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            try
            {
                List<List<string>> lijsten = loadExcelNames();
                List<string> voornamen = lijsten[0];
                List<string> achternamen = lijsten[1];

                int aantal_spelers = 0;
                Console.WriteLine("Geef het aantal spelers in die je wilt genereren : \n");
                aantal_spelers = Convert.ToInt16(Console.ReadLine());
                
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
                        UserNameSpel userName = new UserNameSpel(number,usernaam);
                        userNames.Add(userName);
                    }
                    model.FirstName = voornaam;
                    model.LastName = achternaam;
                    model.Email = voornaam + "_" + achternaam + "@gmail.com";
                    model.Password = voornaam + achternaam;
                    model.userNamePerSpel = userNames;
                    var result = new WorldWideGamerMVC.Controllers.AccountController().Register(model, gameIds).GetAwaiter();
                    //System.Threading.Thread.Sleep(5000);
                    if (result.IsCompleted)
                    {
                        Console.WriteLine("end speler {0}\n", i);
                    }
                }
                System.Threading.Thread.Sleep(15000);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

        public static List<List<string>> loadExcelNames()
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

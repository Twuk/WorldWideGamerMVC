using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class UserNameSpel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpelerGameId { get; set; }
        [Key, ForeignKey("UserId")]
        public Speler speler { get; set; }
        public string UserId { get; set; }
        [Key, ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }
        public string UserName { get; set; }


        public UserNameSpel()
        {

        }

        public UserNameSpel(int gameId, string userName)
        {
            GameId = gameId;
            UserName = userName;
        }

        public UserNameSpel(Game game, int gameId, string userName)
        {
            Game = game;
            GameId = gameId;
            UserName = userName;
        }
    }
    
}
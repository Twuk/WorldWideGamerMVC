using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldWideGamerMVC.Models.Tables
{
    public class Speler
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<UserNameSpel> SpeeltGames { get; set; }

        

        // other fields...

        public virtual ApplicationUser User { get; set; }
    }
}
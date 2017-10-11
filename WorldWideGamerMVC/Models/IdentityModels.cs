
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WorldWideGamerMVC.Models.Tables;
using System.Data.Entity;

namespace WorldWideGamerMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public Speler speler { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext()
            : base("wwg", throwIfV1Schema: false)
        {
        }
        
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<GeschiedenisDetails> GeschiedenisSpelers { get; set; }
        public DbSet<UserNameSpel> SpelerGames { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Speler> Spelers { get; set; }
        public DbSet<Geschiedenis> Geschiedenissen { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Models;

namespace TestMvcProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Anime> Animies { get; set; }
        public DbSet<Manga> Mangas { get; set; }
    }
    
}

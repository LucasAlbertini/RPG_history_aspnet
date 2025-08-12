using Microsoft.EntityFrameworkCore;
using RPGAPI.Models;

namespace RPGAPI.Data
{
    public class RPGDbContext(DbContextOptions<RPGDbContext> options) :  DbContext(options)
    {
        public DbSet<Faction> Factions => Set<Faction>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Goal> Goals => Set<Goal>();
    }
}

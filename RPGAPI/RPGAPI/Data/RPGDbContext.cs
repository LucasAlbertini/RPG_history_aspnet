using Microsoft.EntityFrameworkCore;
using RPGAPI.Core.Entities;

namespace RPGAPI.Data
{
    public class RPGDbContext(DbContextOptions<RPGDbContext> options) :  DbContext(options)
    {
        public DbSet<Faction> Factions => Set<Faction>();
        public DbSet<Goal> Goals => Set<Goal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faction>()
                .HasData(
                    new Faction { Id = 1, Name = "Knights of Valor", Resources = ["Big army", "Love of the people"] },
                    new Faction { Id = 2, Name = "Wizards of the Arcane", Resources = ["Big library", "A lot of money"] },
                    new Faction { Id = 3, Name = "Rangers of the Wild", Resources = ["Syvian magic"] }
                );
            modelBuilder.Entity<Goal>()
                .HasData(
                    new Goal { Id = 1, Name = "Defender o Reino", Description = "Proteger o reino contra invasores.", ProgressMade = 0, ProgressNecessary = 100, FactionId = 1 },
                    new Goal { Id = 2, Name = "Descobrir Artefato Antigo", Description = "Encontrar o artefato lendário perdido.", ProgressMade = 0, ProgressNecessary = 50, FactionId = 2 },
                    new Goal { Id = 3, Name = "Expandir Território", Description = "Conquistar novas terras para a facção.", ProgressMade = 0, ProgressNecessary = 75, FactionId = 1 },
                    new Goal { Id = 4, Name = "Aprimorar Magia", Description = "Desenvolver um novo feitiço poderoso.", ProgressMade = 0, ProgressNecessary = 40, FactionId = 2 },
                    new Goal { Id = 5, Name = "Aliança com Elfos", Description = "Firmar uma aliança estratégica com os elfos.", ProgressMade = 0, ProgressNecessary = 30, FactionId = 3 }
                );
        }
    }
}

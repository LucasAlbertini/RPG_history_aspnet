using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGAPI.Data;
using RPGAPI.Models;
using RPGAPI.Models.DTOs;
using System;
namespace RPGAPI.Services

{
    public class FactionServices
    {
        private readonly RPGDbContext _context;
        public FactionServices(RPGDbContext context)
        {
            _context = context;
        }
        public async Task<List<FactionDto>> GetFactionsAsync()
        {
            return await _context.Factions
        .Select(f => new FactionDto
        {
            Id = f.Id,
            Name = f.Name,
            Resources = f.Resources,
            Goals = f.Goals.Select(g => new GoalDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                ProgressMade = g.ProgressMade,
                ProgressNecessary = g.ProgressNecessary
            }).ToList()
        })
        .ToListAsync();
        }
        public async Task<FactionDto?> GetFactionByIdAsync(int id)
        {
            return await _context.Factions
                .Where(f => f.Id == id)
                .Select(f => new FactionDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Resources = f.Resources,
                    Goals = f.Goals.Select(g => new GoalDto
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        ProgressMade = g.ProgressMade,
                        ProgressNecessary = g.ProgressNecessary
                    }).ToList()
                })
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<FactionDto> CreateFactionAsync(FactionDto factionDto)
        {
            var faction = new Faction
            {
                Name = factionDto.Name,
                Resources = factionDto.Resources,
                Goals = factionDto.Goals.Select(g => new Goal
                {
                    Name = g.Name,
                    Description = g.Description,
                    ProgressMade = g.ProgressMade,
                    ProgressNecessary = g.ProgressNecessary
                }).ToList()
            };
            _context.Factions.Add(faction);
            foreach (var goal in faction.Goals)
            {
                _context.Goals.Add(goal);
            }
            await _context.SaveChangesAsync();
            FactionDto result = new FactionDto
            {
                Id = faction.Id,
                Name = faction.Name,
                Resources = faction.Resources,
                Goals = faction.Goals.Select(g => new GoalDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    ProgressMade = g.ProgressMade,
                    ProgressNecessary = g.ProgressNecessary
                }).ToList()
            };
            return result;
        }

        public async Task<bool> UpdateFactionAsync(int id, FactionDto factionDto)
        {
            var faction = _context.Factions
                .Include(f => f.Goals)
                .FirstOrDefault(f => f.Id == id);

            if (faction == null)
                return false;

            faction.Name = factionDto.Name;
            faction.Resources = factionDto.Resources;

            // Atualiza, remove e adiciona metas conforme necessário
            // Remove metas que não existem mais
            List<Goal> goalsToRemove = faction.Goals
                .Where(g => !factionDto.Goals.Any(dto => dto.Id == g.Id))
                .ToList();
            _context.Goals.RemoveRange(goalsToRemove);

            // Atualiza ou adiciona metas
            foreach (var goalDto in factionDto.Goals)
            {
                var existingGoal = faction.Goals.FirstOrDefault(g => g.Id == goalDto.Id);
                if (existingGoal != null)
                {
                    // Atualiza meta existente
                    existingGoal.Name = goalDto.Name;
                    existingGoal.Description = goalDto.Description;
                    existingGoal.ProgressMade = goalDto.ProgressMade;
                    existingGoal.ProgressNecessary = goalDto.ProgressNecessary;
                }
                else
                {
                    // Adiciona nova meta
                    faction.Goals.Add(new Goal
                    {
                        Name = goalDto.Name,
                        Description = goalDto.Description,
                        ProgressMade = goalDto.ProgressMade,
                        ProgressNecessary = goalDto.ProgressNecessary,
                        FactionId = faction.Id
                    });
                }
            }

            _context.Factions.Update(faction);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFactionAsync(int id)
        {
            Faction? faction = _context.Factions
                .Include(f => f.Goals)
                .FirstOrDefault(f => f.Id == id);
            if (faction == null)
            {
                return false;
            }
            _context.Goals.RemoveRange(faction.Goals);
            _context.Factions.Remove(faction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

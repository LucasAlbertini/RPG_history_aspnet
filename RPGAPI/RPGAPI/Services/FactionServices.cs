using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGAPI.Core.Entities;
using RPGAPI.Data;
using RPGAPI.Mapping;
using RPGAPI.Models.DTOs;
using System;
using System.Runtime.Intrinsics.X86;
namespace RPGAPI.Services

{
    public class FactionServices
    {
        private readonly RPGDbContext _context;
        private readonly IMapper _mapper;
        public FactionServices(RPGDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FactionDto>> GetFactionsAsync()
        {
            return _mapper.Map<List<FactionDto>>(
                await _context.Factions
                    .Include(f => f.Goals)
                    .ToListAsync());
        }
        public async Task<FactionDto?> GetFactionByIdAsync(int id)
        {
            Faction? faction = await _context.Factions
                .Include(f => f.Goals)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faction == null)
                return null;

            return _mapper.Map<FactionDto>(faction);
        }
        public async Task<FactionDto> CreateFactionAsync(FactionDto factionDto)
        {
            // Usa o AutoMapper para mapear o DTO para a entidade
            Faction faction = _mapper.Map<Faction>(factionDto);

            _context.Factions.Add(faction);
            foreach (var goal in faction.Goals)
            {
                _context.Goals.Add(goal);
            }
            await _context.SaveChangesAsync();

            // Usa o AutoMapper para mapear a entidade salva de volta para DTO
            var result = _mapper.Map<FactionDto>(faction);
            return result;
        }

        public async Task<bool> UpdateFactionAsync(int id, FactionDto factionDto)
        {
            var faction = _context.Factions
                .Include(f => f.Goals)
                .FirstOrDefault(f => f.Id == id);

            if (faction == null)
                return false;

            //faction = _mapper.Map<Faction>(factionDto); 
            //Como é uma instancia ja existente, posso fazer dessa forma:
            _mapper.Map(factionDto, faction);
            faction.Id = id;

            // Atualiza, remove e adiciona metas conforme necessário
            // Remove metas que não existem mais
            List<Goal> goalsToRemove = faction.Goals
                .Where(g => !factionDto.Goals.Any(dto => dto.Id == g.Id))
                .ToList();
            _context.Goals.RemoveRange(goalsToRemove);

            // Atualiza ou adiciona metas
            foreach (GoalDto goalDto in factionDto.Goals)
            {
                Goal? existingGoal = faction.Goals.FirstOrDefault(g => g.Id == goalDto.Id);
                if (existingGoal != null)
                {
                    // Atualiza meta existente
                    existingGoal = _mapper.Map<Goal>(goalDto);
                }
                else
                {
                    // Adiciona nova meta
                    Goal newGoal = _mapper.Map<Goal>(goalDto);
                    newGoal.FactionId = faction.Id;
                    faction.Goals.Add(newGoal);
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

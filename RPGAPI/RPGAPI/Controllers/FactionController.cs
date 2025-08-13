using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGAPI.Data;
using RPGAPI.Models;

namespace RPGAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Exemplo de controller com injeção de dependência com primary constructor
    //public class FactionController(RPGDbContext context) : ControllerBase
    //{
    //    private readonly RPGDbContext _context = context;

    public class FactionController : ControllerBase
    {
        private readonly RPGDbContext _context;
        public FactionController(RPGDbContext context)
        {
            _context = context;
        }

        // ...

        [HttpGet]
        public async Task<ActionResult<List<Faction>>> GetFactions()
        {
            List<Faction> factions = await _context.Factions
                .Include(f => f.Goals)
                .ToListAsync();

            return Ok(factions);
        }

        //[HttpGet("{id}")]
        //public ActionResult<Faction> GetFactionById(int id)
        //{
        //    Faction? faction = factions.FirstOrDefault(f => f.Id == id);
        //    if (faction == null)
        //    {
        //        return NotFound($"Faction with ID {id} not found.");
        //    }
        //    return Ok(faction);
        //}
        //[HttpPost]
        //public ActionResult<Faction> PostFaction(Faction faction)
        //{
        //    if (faction is null)
        //    {
        //        return BadRequest();
        //    }
        //    faction.Id = factions.Max(g => g.Id) + 1;
        //    factions.Add(faction);
        //    return CreatedAtAction(nameof(GetFactionById), new { id = faction.Id }, faction);
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateFaction(int id, Faction updatedFaction)
        //{
        //    Faction? faction = factions.FirstOrDefault(f => f.Id == id);
        //    if (faction == null)
        //    {
        //        return NotFound($"Faction with ID {id} not found.");
        //    }
        //    (faction.Name, faction.Resources, faction.Goals) = (updatedFaction.Name, updatedFaction.Resources, updatedFaction.Goals);
        //    return NoContent();
        //}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteFaction(int id)
        //{
        //    Faction? faction = factions.FirstOrDefault(f => f.Id == id);
        //    if (faction == null)
        //    {
        //        return NotFound($"Faction with ID {id} not found.");
        //    }
        //    factions.Remove(faction);
        //    return NoContent();
        //}
    }
}

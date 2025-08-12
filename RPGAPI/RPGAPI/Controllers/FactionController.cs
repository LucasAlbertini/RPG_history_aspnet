using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RPGAPI.Models;

namespace RPGAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactionController : ControllerBase
    {
        static List<Faction> factions = new List<Faction>
        {
            new Faction { Id = 1, Name = "Knights of Valor", Resources = new List<Resource>(), Goals = new List<Goal>() },
            new Faction { Id = 2, Name = "Wizards of the Arcane", Resources = new List<Resource>(), Goals = new List<Goal>() },
            new Faction { Id = 3, Name = "Rangers of the Wild", Resources = new List<Resource>(), Goals = new List<Goal>() }
        };
        [HttpGet]
        public ActionResult<List<Faction>> GetFactions()
        {
            // This is a placeholder for the actual implementation.
            // In a real application, you would retrieve the factions from a database or other data source.
            return Ok(factions);
        }

        [HttpGet("{id}")]
        public ActionResult<Faction> GetFactionById(int id)
        {
            Faction? faction = factions.FirstOrDefault(f => f.Id == id);
            if (faction == null)
            {
                return NotFound($"Faction with ID {id} not found.");
            }
            return Ok(faction);
        }
        [HttpPost]
        public ActionResult<Faction> PostFaction(Faction faction)
        {
            if (faction is null)
            {
                return BadRequest();
            }
            faction.Id = factions.Max(g => g.Id) + 1;
            factions.Add(faction);
            return CreatedAtAction(nameof(GetFactionById), new { id = faction.Id }, faction);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFaction(int id, Faction updatedFaction)
        {
            Faction? faction = factions.FirstOrDefault(f => f.Id == id);
            if (faction == null)
            {
                return NotFound($"Faction with ID {id} not found.");
            }
            (faction.Name, faction.Resources, faction.Goals) = (updatedFaction.Name, updatedFaction.Resources, updatedFaction.Goals);
            return NoContent();
        }
    }
}

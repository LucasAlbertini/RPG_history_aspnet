using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RPGAPI.Models;
using RPGAPI.Models.DTOs;
using RPGAPI.Services;
using System.Threading.Tasks;

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
        private readonly FactionServices _factionServices;
        public FactionController(FactionServices factionServices)
        {
            _factionServices = factionServices;
        }

        // ...

        [HttpGet]
        public async Task<ActionResult<List<FactionDto>>> GetFactions()
        {
            try
            {
                List<FactionDto> factions = await _factionServices.GetFactionsAsync();
                return Ok(factions);
            }
            catch (Exception)
            {
                // Retorna erro 500 com mensagem genérica
                //return StatusCode(500, "Erro interno ao buscar facções.");
                // Ou, se preferir, pode usar:
                return Problem("Erro interno ao buscar facções.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FactionDto>> GetFactionById(int id)
        {
            FactionDto? faction = await _factionServices.GetFactionByIdAsync(id);
            if (faction == null)
            {
                return NotFound($"Faction with id {id} not found.");
            }
            return Ok(faction);
        }

        [HttpPost]
        public async Task<ActionResult<FactionDto>> PostFaction(FactionDto factionDto)
        {
            if (factionDto is null)
            {
                return BadRequest();
            }

            FactionDto response = await _factionServices.CreateFactionAsync(factionDto);

            return CreatedAtAction(nameof(GetFactionById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaction(int id, FactionDto updatedFaction)
        {
            if (updatedFaction is null)
            {
                return BadRequest();
            }
            if (await _factionServices.UpdateFactionAsync(id, updatedFaction))
            {
                return NoContent();
            }
            return Problem("Erro interno ao buscar facção.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaction(int id)
        {
            if(await _factionServices.DeleteFactionAsync(id))
            {
                return NoContent();
            }
            return NotFound($"Faction with ID {id} not found.");
        }
    }
}

using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivresController : Controller
    {
        private readonly LivreServices _livreService;

        public LivresController(LivreServices livreService)
        {
            _livreService = livreService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok!", typeof(GetLivreDTO))]
        public async Task<ActionResult<IEnumerable<GetLivreDTO>>> GetAll()
        {
            var livres = await _livreService.AfficherLivreAsync();
            return Ok(livres);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(GetLivreDTO))]
        public async Task<ActionResult<GetLivreDTO>> GetById(int id)
        {
            var livre = await _livreService.GetLivreByIdAsync(id);
            if (livre == null)
            {
                return NotFound($"Le livre avec l'ID {id} est introuvable.");
            }
            return Ok(livre);
        }

        [HttpPost]
        [SwaggerResponse(201, "Créé avec succès!", typeof(PostLivreDTO))]
        public async Task<ActionResult<PostLivreDTO>> AddLivreAsync(PostLivreDTO livre)
        {
            var newLivre = await _livreService.AddLivreAsync(livre);
            return CreatedAtAction(nameof(GetById), newLivre);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "Mis à jour avec succès!", typeof(PostLivreDTO))]
        public async Task<IActionResult> PutLivre(int id, PostLivreDTO livre)
        {
            await _livreService.UpdateLivreAsync(id, livre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Supprimé avec succès!")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            await _livreService.DeleteLivreAsync(id);
            return NoContent();
        }

        [HttpGet("disponibles")]
        [SwaggerResponse(200, "Ok!", typeof(List<GetLivreDTO>))]
        public async Task<IActionResult> GetLivresDisponibles()
        {
            var livresDisponibles = await _livreService.GetLivresDisponiblesAsync();
            return Ok(livresDisponibles);
        }
    }
}
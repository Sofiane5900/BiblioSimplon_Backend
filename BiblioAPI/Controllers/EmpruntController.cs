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
    [Route("api/[controller]")]
    [ApiController]
    public class EmpruntController : Controller
    {
        private readonly EmpruntServices _empruntServices;

        public EmpruntController(EmpruntServices empruntServices)
        {
            _empruntServices = empruntServices;
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok!", typeof(GetEmpruntDTO))]
        public async Task<IActionResult> GetEmprunts()
        {
            var emprunts = await _empruntServices.AfficherEmprunts();
            // Si il n'y a pas d'emprunts
            if (emprunts.Count == 0)
            {
                return NotFound("Il n'y a pas d'emprunts");
            }
            return Ok(emprunts);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(GetEmpruntDTO))]
        public async Task<IActionResult> GetEmpruntsById(int id)
        {
            var emprunt = await _empruntServices.AfficherEmpruntId(id);
            if (emprunt is null)
            {
                return NotFound("Il n'y a pas d'emprunt avec cet Id");
            }
            return Ok(emprunt);
        }

        [HttpPost]
        [SwaggerResponse(200, "Ok!", typeof(PostEmpruntDTO))]
        public async Task<IActionResult> CreateEmprunt(PostEmpruntDTO emprunt)
        {
            var nouveauEmprunt = await _empruntServices.AjouterEmprunt(
                emprunt.MembreId,
                emprunt.LivreId
            );
            // TODO : Gérer les erreurs individuellement, un message spécifique a chaque erreur
            if (nouveauEmprunt is null)
            {
                return NotFound("Le livre ou le membre n'existe pas");
            }
            return Ok(nouveauEmprunt);
        }

        [HttpDelete]
        public IActionResult DeleteEmprunt(int Id)
        {
            _empruntServices.RendreLivre(Id);
            return NoContent();
        }

        [HttpGet("membre/{membreId}")]
        [SwaggerResponse(200, "Ok!", typeof(GetEmpruntDTO))]
        public async Task<IActionResult> GetEmpruntsByMembre(int membreId)
        {
            var emprunts = await _empruntServices.ConsulterEmpruntsParMembre(membreId);
            if (emprunts is null)
            {
                return NotFound("Il n'y a pas d'emprunts pour ce membre");
            }
            return Ok(emprunts);
        }
    }
}

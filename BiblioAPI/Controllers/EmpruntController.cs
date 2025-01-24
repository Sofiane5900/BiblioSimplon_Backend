using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("empruntsActif")]
        //[Authorize(Roles = "Admin,Bibliothecaire")]
        [SwaggerResponse(200, "Ok!", typeof(GetEmpruntDTO))]
        public async Task<IActionResult> GetEmpruntsActif()
        {
            var emprunts = await _empruntServices.AfficherEmpruntsActif();
            // Si il n'y a pas d'emprunts
            if (emprunts.Count == 0)
            {
                return NotFound("Il n'y a pas d'emprunts");
            }
            return Ok(emprunts);
        }

        [HttpGet("empruntsInactif")]
        [Authorize(Roles = "Admin,Bibliothecaire")]
        [SwaggerResponse(200, "Ok!", typeof(GetEmpruntDTO))]
        public async Task<IActionResult> GetEmpruntsInactif()
        {
            var emprunts = await _empruntServices.AfficherEmpruntsInactif();
            // Si il n'y a pas d'emprunts
            if (emprunts.Count == 0)
            {
                return NotFound("Il n'y a pas d'emprunts");
            }
            return Ok(emprunts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Bibliothecaire")]
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
        //[Authorize(Roles = "Admin,Bibliothecaire")]
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

        [HttpPut("rendreEmprunt/{id}")]
        [Authorize(Roles = "Admin,Bibliothecaire")]
        [SwaggerResponse(200, "Ok!", typeof(PostEmpruntDTO))]
        public async Task<IActionResult> RendreEmprunt(int Id)
        {
            var emprunt = await _empruntServices.RendreEmprunt(Id);
            if (emprunt is null)
            {
                return NotFound("Il n'y a pas d'emprunt avec cet Id");
            }
            _empruntServices.RendreEmprunt(Id);
            return Ok("L'emprunt a été rendu");
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin,Bibliothecaire")]
        public IActionResult DeleteEmprunt(int Id)
        {
            _empruntServices.DeleteEmprunt(Id);
            return NoContent();
        }

        [HttpGet("membre/{membreId}")]
        [Authorize(Roles = "Admin,Bibliothecaire")]
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

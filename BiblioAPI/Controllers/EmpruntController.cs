using BiblioAPI.Data;
using BiblioAPI.Models;
using BiblioAPI.Services;
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
        public async Task<IActionResult> GetEmpruntsById(int Id)
        {
            var emprunt = await _empruntServices.AfficherEmpruntId(Id);
            return Ok(emprunt);
        }

        [HttpPost]
        [SwaggerResponse(200, "Ok!", typeof(PostEmpruntDTO))]
        public async Task<IActionResult> CreateEmprunt(PostEmpruntDTO emprunt)
        {
            var nouveauEmprunt = await _empruntServices.EmprunterLivre(
                emprunt.MembreId,
                emprunt.LivreId
            );
            return Ok(nouveauEmprunt);
        }

        [HttpPut]
        [SwaggerResponse(200, "Ok!", typeof(PostEmpruntDTO))]
        public async Task<IActionResult> UpdateEmprunt(int Id, PostEmpruntDTO emprunt)
        {
            var empruntAModifier = await _empruntServices.ModifierEmprunt(Id, emprunt);
            return Ok(empruntAModifier);
        }

        [HttpPut]
        [SwaggerResponse(200, "Ok!", typeof(PostEmpruntDTO))]
        public async Task<IActionResult> UpdateEmprunt(int Id, PostEmpruntDTO emprunt)
        {
            var empruntAModifier = await _empruntServices.ModifierEmprunt(Id, emprunt);
            return Ok(empruntAModifier);
        }
    }
}

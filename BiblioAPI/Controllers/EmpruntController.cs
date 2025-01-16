using BiblioAPI.Data;
using BiblioAPI.Models;
using BiblioAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetEmpruntsById(int Id)
        {
            var emprunt = await _empruntServices.AfficherEmpruntId(Id);
            return Ok(emprunt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmprunt(EmpruntModelCreateDTO empruntModelCreateDTO)
        {
            var emprunt = await _empruntServices.AjouterEmprunt(empruntModelCreateDTO);
            return Ok(emprunt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmprunt(
            int Id,
            EmpruntModelCreateDTO empruntModelCreateDTO
        )
        {
            var emprunt = await _empruntServices.ModifierEmprunt(Id, empruntModelCreateDTO);
            return Ok(emprunt);
        }
    }
}

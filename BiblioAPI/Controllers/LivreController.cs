using BiblioAPI.Models;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiblioAPI.Controllers
{
    
    public class LivresController : ControllerBase
    {
        private readonly ILivreService _livreService;

        public LivresController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var livres = _livreService.GetAllLivres();
            return Ok(livres);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var livre = _livreService.GetLivreById(id);
            if (livre == null)
            {
                return NotFound();
            }
            return Ok(livre);
        }

        [HttpPost]
        public IActionResult Add([FromBody] LivreModel livre)
        {
            _livreService.AddLivre(livre);
            return CreatedAtAction(nameof(GetById), new { id = livre.Id }, livre);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LivreModel livre)
        {
            _livreService.UpdateLivre(id, livre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _livreService.DeleteLivre(id);
            return NoContent();
        }
    }
}
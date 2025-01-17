using BiblioAPI.Models;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : Controller
    {
        private readonly ILivreService _livreService;

        public LivresController(ILivreService livreService)
        {
            _livreService = livreService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok!", typeof(GetLivreDTO))]
        public IActionResult GetAll()
        {
            var livres = _livreService.GetAllLivres();
            return Ok(livres);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(GetLivreDTO))]
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
        [SwaggerResponse(200, "Ok!", typeof(PostLivreDTO))]
        public IActionResult PostLivre(PostLivreDTO livre)
        {
            _livreService.AddLivre(livre);
            return CreatedAtAction(nameof(GetById), livre);
        }

        [HttpPut]
        [SwaggerResponse(200, "Ok!", typeof(PostLivreDTO))]
        public IActionResult PutLivre( int Id,PostLivreDTO livre)
        {
            _livreService.UpdateLivre(Id, livre);
            return NoContent();
        }

        [HttpDelete]
        [SwaggerResponse(200, "Ok!", typeof(PostLivreDTO))]
        public IActionResult DeleteLivre(int Id)
        {
            _livreService.DeleteLivre(Id);
            return NoContent();
        }
    }
}

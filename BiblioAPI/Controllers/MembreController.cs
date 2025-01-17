using BiblioAPI.Models;
using BiblioAPI.Services;
using BiblioAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembreController : Controller
    {
        private readonly MembreService _membreService;

        public MembreController(MembreService membreService)
        {
            _membreService = membreService;
        }

        // GET: api/Membre
        [HttpGet]
        [SwaggerResponse(200, "Ok!", typeof(GetMembreDTO))]
        public async Task<ActionResult<IEnumerable<GetMembreDTO>>> GetAllMembers()
        {
            var membres = await _membreService.GetAllMembersAsync();
            return Ok(membres);
        }

        // GET: api/Membre/{id}
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(GetMembreDTO))]
        public async Task<ActionResult<GetMembreDTO>> GetMemberById(int id)
        {
            var membre = await _membreService.GetMemberByIdAsync(id);

            if (membre == null)
            {
                return NotFound(new { Message = $"Member with ID {id} not found." });
            }

            return Ok(membre);
        }

        //POST: api/Membre
        [HttpPost]
        [SwaggerResponse(200, "Ok!", typeof(PostMembreDTO))]
        public async Task<ActionResult<PostMembreDTO>> AddMember(PostMembreDTO membre)
        {
            var newMembre = await _membreService.AddMemberAsync(membre);
            return CreatedAtAction(nameof(GetMemberById), newMembre);
        }

        //PUT: api/Membre/{id}
        [HttpPut("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(PostMembreDTO))]
        public IActionResult PutLivre(int Id, PostMembreDTO membre)
        {
            _membreService.UpdateMemberAsync(Id, membre);
            return NoContent();
        }

        // DELETE: api/Membre/{id}
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Ok!", typeof(PostMembreDTO))]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _membreService.DeleteMemberAsync(id);

            if (!result)
            {
                return NotFound(new { Message = $"Member with ID {id} not found." });
            }

            return NoContent();
        }
    }
}

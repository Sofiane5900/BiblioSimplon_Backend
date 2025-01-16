using Microsoft.AspNetCore.Mvc;
using BiblioAPI.Models;
using BiblioAPI.Services;


    namespace BiblioAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class MembreController : ControllerBase
        {
            private readonly MembreService _membreService;

            public MembreController(MembreService membreService)
            {
                _membreService = membreService;
            }

            // GET: api/Membre
            [HttpGet]
            public async Task<ActionResult<IEnumerable<MembreModel>>> GetAllMembers()
            {
                var membres = await _membreService.GetAllMembersAsync();
                return Ok(membres);
            }

            // GET: api/Membre/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<MembreModel>> GetMemberById(int id)
            {
                var membre = await _membreService.GetMemberByIdAsync(id);

                if (membre == null)
                {
                    return NotFound(new { Message = $"Member with ID {id} not found." });
                }

                return Ok(membre);
            }

            // POST: api/Membre
            [HttpPost]
            public async Task<ActionResult<MembreModel>> AddMember(MembreModel membre)
            {
                var newMembre = await _membreService.AddMemberAsync(membre);
                return CreatedAtAction(nameof(GetMemberById), new { id = newMembre.Id }, newMembre);
            }

            // PUT: api/Membre/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateMember(int id, MembreModel membre)
            {
                if (id != membre.Id)
                {
                    return BadRequest(new { Message = "The ID in the URL does not match the member ID." });
                }

                var result = await _membreService.UpdateMemberAsync(id, membre);

                if (!result)
                {
                    return NotFound(new { Message = $"Member with ID {id} not found." });
                }

                return NoContent();
            }

            // DELETE: api/Membre/{id}
            [HttpDelete("{id}")]
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

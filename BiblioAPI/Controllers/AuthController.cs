using System.IdentityModel.Tokens.Jwt;
using BiblioAPI.Interfaces;
using System.Security.Claims;
using BiblioAPI.Models;
using BiblioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BiblioAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace BiblioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authServices;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthServices authServices, ILogger<AuthController> logger)
        {
            _authServices = authServices;
            _logger = logger;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterEmploye(RegisterEmployeDTO employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authServices.RegisterEmploye(employe);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginEmploye(LoginEmployeDTO employe)
        {
            var employeLogin = await _authServices.LoginEmploye(employe);

            if (employeLogin == null)
            {
                return Unauthorized(); // 401
            }

            // Genere un JWT token si le login est successfull
            var token = _authServices.GenerateJwtToken(employeLogin);

            // On affiche le token géneré dans la reponse
            return Ok(new { Token = token });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(200, "Ok!", typeof(GetEmployeDTO))]
        public async Task<ActionResult<IEnumerable<GetEmployeDTO>>> GetAll()
        {
            var employe = await _authServices.AfficherEmployerAsync();
            return Ok(employe);
        }

        [HttpPost("reset-password")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(204, "Mis à jour avec succès!", typeof(RegisterEmployeDTO))]
        
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO request)
{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authServices.ResetUserPasswordAsync(request.Username, request.NewPassword);

            if (result == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
    }
}

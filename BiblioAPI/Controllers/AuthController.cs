using System.IdentityModel.Tokens.Jwt;
using BiblioAPI.Models;
using BiblioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
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
    }
}

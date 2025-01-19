using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BiblioAPI.Data;
using BiblioAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BiblioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController
    {
        private readonly IConfiguration _config;
        private readonly BiblioDbContext _context;

        public AuthController(IConfiguration config, BiblioDbContext context)
        {
            _config = config;
            _context = context;
        }
    }
}

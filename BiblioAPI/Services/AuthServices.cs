using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BiblioAPI.Data;
using BiblioAPI.Interfaces;
using BiblioAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BiblioAPI.Services
{
    // TODO : Faire des recherches sur Issuer,Audience,Hash
    public class AuthServices : IAuthService
    {
        private readonly BiblioDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthServices(BiblioDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<RegisterEmployeDTO?> RegisterEmploye(RegisterEmployeDTO employe)
        {
            // On cherche un employé qui a le meme email ou username
            var employeExistant = await _context.Employe.FirstOrDefaultAsync(e =>
                e.Email == employe.Email || e.Username == employe.Username
            );

            // Si mon employé éxiste (email/username similaire), on renvoie null (la méthode s'arretera ici)
            if (employeExistant is not null)
            {
                return null;
            }

            // Si aucun employé n'est trouvé, on en crée un nouveau a partir de notre model
            var nouveauEmploye = new EmployeModel
            {
                Username = employe.Username,
                Email = employe.Email,
                Password = employe.Password, // TODO: hash le mdp
                Role = employe.Role, // TODO; faire du controle de saisie sur le Role,
            };

            // Si tout est good on l'ajoute a la DB
            _context.Employe.Add(nouveauEmploye);
            await _context.SaveChangesAsync();

            return employe;
        }

        public async Task<EmployeModel?> LoginEmploye(LoginEmployeDTO employe)
        {
            // On cherche notre utilisateur par son username
            var employeLogin = await _context.Employe.FirstOrDefaultAsync(e =>
                e.Username == employe.Username
            );

            if (employeLogin == null || employeLogin.Password != employe.Password)
            {
                return null;
            }

            return employeLogin;
        }

        public string GenerateJwtToken(EmployeModel employe)
        {
            // On récupere nos valeurs de notre appsetting.json
            var issuer = _configuration["JWTConfig:Issuer"];
            var audience = _configuration["JWTConfig:Audience"];
            var secretKey = _configuration["JWTConfig:Secret"];
            var expiration = int.Parse(_configuration["JWTConfig:Expiration"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, employe.Username),
                new Claim(ClaimTypes.Email, employe.Email),
                new Claim(ClaimTypes.Role, employe.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiration), //1 heure actuellement, on pourrait l'augmenter dans le futur, a réflechir.
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

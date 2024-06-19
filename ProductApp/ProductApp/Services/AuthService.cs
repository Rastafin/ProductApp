using Microsoft.IdentityModel.Tokens;
using ProductApp.Data;
using ProductApp.Models;
using ProductApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public string Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null!;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = "https://localhost:7144",
                Issuer = "https://localhost:7144",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Register(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

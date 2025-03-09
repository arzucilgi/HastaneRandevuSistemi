
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using randevuSistemi.Dtos;
using randevuSistemi.Models;
using randevuSistemi.Services;

namespace randevuSistemi.Controllers
{
    [ApiController]
   
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);

        private readonly IConfiguration configuration;
        private readonly AppDBContext dbContext;

        public AuthenticationController(AppDBContext dbContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {
            var userObj = dbContext.Kullanicilar.FirstOrDefault(u => u.TcKimlikNo == dto.TcKimlik);
            if (userObj is null)
                return NotFound();

            if (PasswordHasher.HashPassword(dto.Password) != userObj.Sifre)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,userObj.Id.ToString())
            };

            if (userObj.IsAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifeTime),
                Issuer = configuration["JwtSettings:Issuer"],
                Audience = configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);

            HttpContext.Response.Cookies.Append("token", jwt, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            });

            return Ok(new
            {
                UserId = userObj.Id,
                Username = userObj.Name,
                ExpireDate = DateTime.UtcNow.Add(TokenLifeTime),
                Role = userObj.IsAdmin ? "Admin" : "User"
            });

            // return Ok(jwt);
        }

        [HttpPost("Register")]
        public IActionResult Register(KullaniciDtos dto)
        {
            string hashedPassword = PasswordHasher.HashPassword(dto.Sifre);

            var userObj = new Kullanici()
            {
                TcKimlikNo=dto.TcKimlikNo,
                Name = dto.Name,
                Surname=dto.Surname,
                AnneName=dto.AnneName,
                BabaName=dto.BabaName,
                Cinsiyet=dto.Cinsiyet,
                DogumTarihi=dto.DogumTarihi,
                DogumYeri=dto.DogumYeri,
                Telefon=dto.Telefon,
                Sifre = hashedPassword,
                IsAdmin = dto.IsAdmin
            };

            if (dbContext.Kullanicilar.Any(u => u.TcKimlikNo == dto.TcKimlikNo))
                     return Conflict("Kullanıcı zaten mevcut.");
            dbContext.Kullanicilar.Add(userObj);
            dbContext.SaveChanges();

            return Ok(userObj);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("token", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                HttpOnly = true
            });
            return Ok();
        }
         [HttpGet]
        [Route("{Id:int}")]
        public IActionResult getKullaniciById(int Id)
        {
            var kullaniciObj = dbContext.Kullanicilar.Find(Id);
            if (kullaniciObj is null)
                return NotFound();
            return Ok(kullaniciObj);
        }

    }
}
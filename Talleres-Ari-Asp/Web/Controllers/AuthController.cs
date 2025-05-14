using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.ValidateUser(request.UserName, request.Password);

            if (user == null)
                return Unauthorized("Credenciales incorrectas");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            return Ok(new { Message = "Token válido" });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                RevokedTokens.Add(token);
                return Ok(new { Message = "Sesión cerrada correctamente" });
            }

            return BadRequest("Token no proporcionado");
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized("No se pudo validar el token.");

            int userId = int.Parse(userIdClaim);

            var user = _userService.GetById(userId);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            user.UserName = request.NewUserName ?? user.UserName;

            if (!string.IsNullOrWhiteSpace(request.NewPassword))
                user.PasswordHash = _userService.HashPassword(request.NewPassword!);

            _userService.Update(user);

            return Ok("Usuario actualizado exitosamente.");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var existingUser = _userService.GetByUserName(request.UserName);
            if (existingUser != null)
                return BadRequest("El nombre de usuario ya existe.");

            var newUser = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _userService.Add(newUser);
            return Ok("Usuario registrado correctamente");
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult Update([FromBody] UpdateRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = _userService.GetById(userId);

            if (user == null)
                return NotFound("Usuario no encontrado.");

            if (!string.IsNullOrEmpty(request.UserName))
                user.UserName = request.UserName;

            if (!string.IsNullOrEmpty(request.NewPassword))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            _userService.Update(user);
            return Ok("Usuario actualizado correctamente");
        }



public class UpdateRequest
{
    public string? NewUserName { get; set; }
    public string? NewPassword { get; set; }
}

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                // Puedes agregar más claims si quieres como roles
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

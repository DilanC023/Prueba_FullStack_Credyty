using App.Servicios.DTOs;
using App.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IConfiguration _config;

        public AutenticacionController(IUsuarioServicio usuarioServicio, IConfiguration config)
        {
            _usuarioServicio = usuarioServicio;
            _config = config;
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        public async Task<IActionResult> AdicionarUsuario(UsuarioDTO usuario)
        {
            var resultado = await _usuarioServicio.AdicionarUsuario(usuario);
            if (!resultado)
                return BadRequest("No se pudo registrar el usuario.");

            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AutenticacionDTO login)
        {
            var usuario = await _usuarioServicio.ObtenerUsuarios();
            var existeUsuario = usuario.FirstOrDefault(x => x.Email == login.Email && x.Clave == login.Clave);
            if (existeUsuario == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = GenerarToken(existeUsuario);
            return Ok(new 
            {
                Token = token,
                Tipo = "Bearer",
                Expira = DateTime.Now.AddMinutes(29).ToString("dd/MM/yyyy'T'HH':'mm':'ss"),

            });
        }

        private string GenerarToken(UsuarioDTO usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

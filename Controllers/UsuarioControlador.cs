using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Colegio.Modelos.Usuario.Procedimientos;
using Colegio.Modelos.Usuario.Salidas_Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Colegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Usuario")]
    public class UsuarioControlador : ControllerBase
    {
        private readonly UsuarioServicio usuarioServicio;
        private readonly IConfiguration configuracion;

        public UsuarioControlador(UsuarioServicio usuarioServicio, IConfiguration configuration)
        {
            this.usuarioServicio = usuarioServicio;
            this.configuracion = configuration;
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesion iniciarSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SalidaIniciarSesion resultadoAutenticacion = await usuarioServicio.ValidarIniciarSesionoAsync(iniciarSesion);

            if (!resultadoAutenticacion.exito)
            {
                return Unauthorized(new { mensaje = resultadoAutenticacion.mensaje });
            }

            var reclamaciones = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuracion["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("nombreUsuario", resultadoAutenticacion.nombreUsuario ?? string.Empty),
                new Claim("rolUsuario", resultadoAutenticacion.nombreRolUsuario ?? string.Empty),
                new Claim("nombreCompleto", resultadoAutenticacion.nombrePersona ?? string.Empty)
            };
            var key = new SymmetricSecurityKey(Convert.FromBase64String(configuracion["Jwt:Key"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: configuracion["Jwt:Issuer"],
                audience: configuracion["Jwt:Audience"],
                claims: reclamaciones,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(configuracion["Jwt:ExpiresInMinutes"] ?? "60")),
                signingCredentials: credenciales
            );

            string tokenValor = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenValor,
                Usuario = new
                {
                    NombreUsuario = resultadoAutenticacion.nombreUsuario,
                    Rol = resultadoAutenticacion.nombreRolUsuario,
                    NombrePersona = resultadoAutenticacion.nombrePersona
                }
            });
        }
    }
}

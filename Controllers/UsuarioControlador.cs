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

        [HttpGet("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromQuery] IniciarSesion iniciarSesion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                SalidaIniciarSesion resultadoAutenticacion = await usuarioServicio.ValidarIniciarSesionoAsync(iniciarSesion);

                if (!resultadoAutenticacion.exito)
                {
                    // 401 Unauthorized si la autenticación falla
                    return Unauthorized(new { mensaje = resultadoAutenticacion.mensaje });
                }

                // Asegurar que la clave exista y sea válida
                string jwtKeyBase64 = configuracion["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "La clave JWT no está configurada.");
                string jwtIssuer = configuracion["Jwt:Issuer"] ?? string.Empty;
                string jwtAudience = configuracion["Jwt:Audience"] ?? string.Empty;
                string jwtSubject = configuracion["Jwt:Subject"] ?? "JwtSubject";

                if (!double.TryParse(configuracion["Jwt:ExpiresInMinutes"], out double expiresInMinutes))
                {
                    expiresInMinutes = 60; // Valor por defecto seguro si la configuración falla
                }

                var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtKeyBase64));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var reclamaciones = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwtSubject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("nombreUsuario", resultadoAutenticacion.nombreUsuario ?? string.Empty),
                    new Claim("rolUsuario", resultadoAutenticacion.nombreRolUsuario ?? string.Empty),
                    new Claim("nombreCompleto", resultadoAutenticacion.nombrePersona ?? string.Empty)
                };

                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: reclamaciones,
                    expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
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
            catch (Exception ex)
            {
                // Devolver 500 Internal Server Error si falla la generación del token o la configuración.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor durante la autenticación: {ex.Message}" });
            }
        }
    }
}
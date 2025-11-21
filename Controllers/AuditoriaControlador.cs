using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auditoria")]
    public class AuditoriaControlador : ControllerBase
    {
        private readonly AuditoriaServicio auditoriaServicio;

        public AuditoriaControlador(AuditoriaServicio auditoriaServicio)
        {
            this.auditoriaServicio = auditoriaServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("{nombreUsuario}/AuditoriaCsv")]
        public async Task<IActionResult> AuditoriaCsv([FromRoute] string nombreUsuario)
        {
            // Validación básica de si el parámetro de ruta fue recibido
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return BadRequest(new { Exito = false, Mensaje = "El nombre de usuario es requerido en la ruta." });
            }

            try
            {
                // 🛑 Llama al método del servicio que devuelve 'bool'
                var resultado = await auditoriaServicio.ValidarAuditoriaCargueCsv(nombreUsuario);

                if (resultado == true)
                {
                    // ✅ Respuesta 200 OK: Devuelve un JSON que solo indica éxito.
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                    });
                }
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error (Fallo del servidor/conexión)
                return StatusCode(500, new { Mensaje = $"Error interno del servidor." });
            }
        }
    }
}

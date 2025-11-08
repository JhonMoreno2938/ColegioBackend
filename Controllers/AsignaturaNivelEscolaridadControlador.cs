using Colegio.Modelos.Asignatura_Nivel_Escolaridad.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "AsignaturaNivelEscolaridad")]
    public class AsignaturaNivelEscolaridadControlador : ControllerBase
    {
        private readonly AsignaturaNivelEscolaridadServicio asignaturaNivelEscolaridadServicio;

        public AsignaturaNivelEscolaridadControlador(AsignaturaNivelEscolaridadServicio asignaturaNivelEscolaridadServicio)
        {
            this.asignaturaNivelEscolaridadServicio = asignaturaNivelEscolaridadServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarAsignatura")]
        public async Task<IActionResult> RegistrarJornadaSede([FromBody] RegistrarAsignatura registrarAsignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await asignaturaNivelEscolaridadServicio.ValidarInformacionRegistrarAsignaturaAsync(registrarAsignatura);

                if (resultado.exito)
                {
                    return CreatedAtAction(nameof(RegistrarJornadaSede), new { Mensaje = resultado.mensaje });
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}

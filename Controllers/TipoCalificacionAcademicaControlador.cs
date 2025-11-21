using Colegio.Modelos.Tipo_Calificacion_Academica.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TipoCalificacionAcademica")]
    public class TipoCalificacionAcademicaControlador : ControllerBase
    {
        private readonly TipoCalificacionAcademicaServicio tipoCalificacionAcademicaServicio;

        public TipoCalificacionAcademicaControlador(TipoCalificacionAcademicaServicio tipoCalificacionAcademicaServicio)
        {
            this.tipoCalificacionAcademicaServicio = tipoCalificacionAcademicaServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarTipoCalificacionAcademica")]
        public async Task<IActionResult> RegistrarTipoCalificacionAcademica([FromBody] RegistrarTipoCalificacionAcademica registrarTipoCalificacionAcademica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await tipoCalificacionAcademicaServicio.ValidarInformacionRegistrarTipoCalificacionAcademicaAsync(registrarTipoCalificacionAcademica);

                if (resultado.exitoso)
                {
                    return CreatedAtAction(nameof(RegistrarTipoCalificacionAcademica), new { Mensaje = resultado.mensaje });
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

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("ModificarInformacionTipoCalificacionAcademica")]
        public async Task<IActionResult> ModificarInformacionTipoCalificacionAcademica([FromBody] ModificarTipoCalificacionAcademica modificarTipoCalificacionAcademica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await tipoCalificacionAcademicaServicio.ValidarModificarInformacionTipoCalificacionAcademicaAsync(modificarTipoCalificacionAcademica);

                if (resultado.exitoso)
                {
                    // 200 OK para una modificación/actualización exitosa.
                    return Ok(new { Mensaje = resultado.mensaje });
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

        [Authorize(Policy = "SecretarioODocente")]
        [HttpGet("InformacionTipoCalificacionAcademica")]
        public async Task<IActionResult> InformacionTipoCalificacionAcademica()
        {
            try
            {
                var listaTipoCalificacionAcademica = await tipoCalificacionAcademicaServicio.ValidarInformacionTipoCalificacionAcademicaAsync();
                return Ok(listaTipoCalificacionAcademica);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }


    }
}

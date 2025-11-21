using Colegio.Modelos.Competencia.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Competencia")]
    public class CompetenciaControlador : ControllerBase
    {
        private readonly CompetenciaServicio competenciaServicio;

        public CompetenciaControlador(CompetenciaServicio competenciaServicio)
        {
            this.competenciaServicio = competenciaServicio;
        }

        [Authorize(Policy = "SoloDocente")]
        [HttpPost("RegistrarCompetencia")]
        public async Task<IActionResult> RegistrarCompetencia([FromBody] RegistrarCompetencia registrarCompetencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await competenciaServicio.ValidarRegistrarCompetenciaAsync(registrarCompetencia);

                if (resultado.exitoso)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(RegistrarCompetencia), new { Mensaje = resultado.mensaje });
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

        [Authorize(Policy = "SoloDocente")]
        [HttpGet("ConsultarCompetenciaAsocadaGradoGrupo")]
        public async Task<IActionResult> ConsultarCompetenciaAsocadaGradoGrupo([FromQuery] ConsultarCompetenciaAsociadaGradoGrupoEntrada consultarCompetenciaAsociadaGradoGrupoEntrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var competenciaAsociadaGradoGrupo = await competenciaServicio.ValidarInformacionCompetenciaAsociadaGradoGrupoAsync(consultarCompetenciaAsociadaGradoGrupoEntrada);
                return Ok(competenciaAsociadaGradoGrupo);
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloDocente")]
        [HttpPut("ModificarDescripcionCompetencia")]
        public async Task<IActionResult> ModificarDescripcionCompetencia([FromBody] ModificarDescripcionCompetencia modificarDescripcionCompetencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await competenciaServicio.ValidarModificarDescripcionCompetenciaAsync(modificarDescripcionCompetencia);

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

        [Authorize(Policy = "SoloDocente")]
        [HttpDelete("{idCompetencia}/EliminarCompetencia")]
        public async Task<IActionResult> EliminarCompetencai([FromRoute] int idCompetencia)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(idCompetencia)))
            {
                return BadRequest(new { Mensaje = "El id de la competencia es requerido." });
            }

            try
            {
                var eliminarCompetencia = await competenciaServicio.ValidarEliminarCompetenciaAsync(idCompetencia);
                return Ok(eliminarCompetencia);
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}

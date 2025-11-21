using Colegio.Modelos.Competencia_Estudiante.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "CompetenciaEstudiante")]
    public class CompetenciaEstudianteControlador : ControllerBase
    {
        private readonly CompetenciaEstudianteServicio competenciaEstudianteServicio;

        public CompetenciaEstudianteControlador(CompetenciaEstudianteServicio competenciaEstudianteServicio)
        {
            this.competenciaEstudianteServicio = competenciaEstudianteServicio;
        }

        [Authorize(Policy = "SoloDocente")]
        [HttpPut("CalificarCompetencia")]
        public async Task<IActionResult> CalificarCompetencia([FromBody] CalificarCompetencia calificarCompetencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 🛑 Llama al método del servicio que devuelve 'bool'
                var resultado = await competenciaEstudianteServicio.ValidarCalificarCompetenciaAsync(calificarCompetencia);

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

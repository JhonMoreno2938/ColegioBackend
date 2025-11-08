using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asignatura")]
    public class AsignaturaControlador : ControllerBase
    {
        private readonly AsignaturaServicio asignaturaServicio;

        public AsignaturaControlador(AsignaturaServicio asignaturaServicio)
        {
            this.asignaturaServicio = asignaturaServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("{nombreAsignatura}/Detalle")]
        public async Task<IActionResult> ConsultarAsignatura([FromRoute] string nombreAsignatura)
        {
            if (string.IsNullOrWhiteSpace(nombreAsignatura))
            {
                return BadRequest(new { Mensaje = "El nombre de la asignatura es requerido para la consulta." });
            }

            try
            {
                var resultado = await asignaturaServicio.ValidarConsultarAsignaturaAsync(nombreAsignatura);

                if (resultado.exito)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("{nombreAsignatura}/GradosActivos")]
        public async Task<IActionResult> ConsultarGradoGrupoAsignaturaEstadoActivo([FromRoute] string nombreAsignatura)
        {
            if (string.IsNullOrWhiteSpace(nombreAsignatura))
            {
                return BadRequest(new { Mensaje = "El nombre de la asignatura es requerido para la consulta." });
            }

            try
            {
                var resultadoLista = await asignaturaServicio.ValidarConsultarGradoGrupoAsignaturaEstadoActivo(nombreAsignatura);

                // 200 OK es la respuesta estándar para una consulta. 
                return Ok(resultadoLista);
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error para fallas de servidor/DB.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionAsignatura")]
        public async Task<IActionResult> InformacionAsignatura()
        {
            try
            {
                var listarAsignatura = await asignaturaServicio.ValidarInformacionAsignaturaAsync();
                return Ok(listarAsignatura);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionAsignaturaEstadoActivo")]
        public async Task<IActionResult> InformacionAsignaturaEstadoActivo()
        {
            try
            {
                var listaAsignaturaEstadoActivo = await asignaturaServicio.ValidarInformacionAsignaturaEstadoActivoAsync();
                return Ok(listaAsignaturaEstadoActivo);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}

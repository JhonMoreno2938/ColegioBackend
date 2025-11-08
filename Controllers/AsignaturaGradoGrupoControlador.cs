using Colegio.Modelos.Asignatura_Grado_Grupo.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "AsignaturaGradoGrupo")]
    public class AsignaturaGradoGrupoControlador : ControllerBase
    {
        private readonly AsignaturaGradoGrupoServicio asignaturaGradoGrupoServicio;

        public AsignaturaGradoGrupoControlador(AsignaturaGradoGrupoServicio asignaturaGradoGrupoServicio)
        {
            this.asignaturaGradoGrupoServicio = asignaturaGradoGrupoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarAsignaturaGradoGrupo")]
        public async Task<IActionResult> RegistrarAsignaturaGradoGrupo([FromBody] RegistrarAsignaturaGradoGrupo registrarAsignaturaGradoGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await asignaturaGradoGrupoServicio.ValidarInformacionRregistrarAsignaturaGradoGrupoAsync(registrarAsignaturaGradoGrupo);

                if (resultado.exito)
                {
                    return CreatedAtAction(nameof(RegistrarAsignaturaGradoGrupo), new { Mensaje = resultado.mensaje });
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
        [HttpPut("GestionarEstadoAsignaturaGradoGrupo")]
        public async Task<IActionResult> GestionarEstadoAsignaturaGradoGrupo([FromBody] GestionarEstadoAsignaturaGradoGrupo gestionarEstadoAsignaturaGradoGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await asignaturaGradoGrupoServicio.ValidarGestionarEstadoAsignaturaGradoGrupoAsync(gestionarEstadoAsignaturaGradoGrupo);

                if (resultado.exito)
                {
                    // Se usa 200 OK para la modificación.
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

    }
}

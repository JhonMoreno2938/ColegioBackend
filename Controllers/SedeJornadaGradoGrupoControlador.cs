using Colegio.Modelos.Sede_Jornada_Grado_Grupo.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "SedeJornadaGradoGrupo")]
    public class SedeJornadaGradoGrupoControlador : ControllerBase
    {
        private readonly SedeJornadaGradoGrupoServicio sedeJornadaGradoGrupoServicio;

        public SedeJornadaGradoGrupoControlador(SedeJornadaGradoGrupoServicio sedeJornadaGradoGrupoServicio)
        {
            this.sedeJornadaGradoGrupoServicio = sedeJornadaGradoGrupoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarGradoGrupoNivelEscolaridadSede")]
        public async Task<IActionResult> RegistrarGradoGrupoNivelEscolaridadSede([FromBody] RegistrarGradoGrupoNivelEscolaridadSede registrarGradoGrupoNivelEscolaridadSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await sedeJornadaGradoGrupoServicio.ValidarInformacionRegistrarGradoGrupoNivelEscolaridadSedeeAsync(registrarGradoGrupoNivelEscolaridadSede);

                if (resultado.exito)
                {
                    return CreatedAtAction(nameof(RegistrarGradoGrupoNivelEscolaridadSede), new { Mensaje = resultado.mensaje });
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
        [HttpPut("GestionarEstadoGradoGrupoSede")]
        public async Task<IActionResult> GestionarEstadoGradoGrupoSede([FromBody] GestionarEstadoGradoGrupoSede gestionarEstadoGradoGrupoSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await sedeJornadaGradoGrupoServicio.ValidarGestionarEstadoSedeJornadaGradpGrupoAsync(gestionarEstadoGradoGrupoSede);

                if (resultado.exito)
                {
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

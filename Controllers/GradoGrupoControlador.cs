using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "GradoGrupo")]
    public class GradoGrupoControlador : ControllerBase
    {
        private readonly GradoGrupoServicio gradoGrupoServicio;

        public GradoGrupoControlador(GradoGrupoServicio gradoGrupoServicio)
        {
            this.gradoGrupoServicio = gradoGrupoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarGradoGrupoNivelEscolaridad")]
        public async Task<IActionResult> RegistrarGradoGrupoNivelEscolaridad([FromBody] RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await gradoGrupoServicio.ValidarInformacionRegistrarGradoGrupoNivelEscolaridadAsync(registrarGradoGrupoNivelEscolaridad);

                if (resultado.exito)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(RegistrarGradoGrupoNivelEscolaridad), new { Mensaje = resultado.mensaje });
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
        [HttpPut("GestionarEstadoGradoGrupoNivelEscolaridad")]
        public async Task<IActionResult> GestionarEstadoGradoGrupoNivelEscolaridad([FromBody] GestionarEstadoGradoGrupoNivelEscolaridad gestionarEstadoGradoGrupoNivelEscolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await gradoGrupoServicio.ValidarGestionarEstadoGradoGrupoNivelEscolaridadAsync(gestionarEstadoGradoGrupoNivelEscolaridad);

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

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionGradoGrupo")]
        public async Task<IActionResult> InformacionGradoGrupo()
        {
            try
            {
                var listaGradoGrupo = await gradoGrupoServicio.ValidarInformacionGradoGrupoAsync();
                return Ok(listaGradoGrupo);
            }
            catch (Exception ex)
            {
                // Se usa 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionGradoGrupoEstadoActivo")]
        public async Task<IActionResult> InformacionGradoGrupoEstadoActivo()
        {
            try
            {
                var listaGradoGrupoEstadoActivo = await gradoGrupoServicio.ValidarInformacionGradoGrupoEstadoActivoAsync();
                return Ok(listaGradoGrupoEstadoActivo);
            }
            catch (Exception ex)
            {
                // Se usa 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
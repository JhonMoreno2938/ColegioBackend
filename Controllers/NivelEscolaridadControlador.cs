using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "NivelEscolaridad")]
    public class NivelEscolaridadControlador : ControllerBase
    {
        private readonly NivelEscolaridadServicio nivelEscolaridadServicio;

        public NivelEscolaridadControlador(NivelEscolaridadServicio nivelEscolaridadServicio)
        {
            this.nivelEscolaridadServicio = nivelEscolaridadServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarNivelEscolaridad")]
        public async Task<IActionResult> RegistrarNivelEscolaridad([FromBody] RegistrarNivelEscolaridad registrarNivelEscolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await nivelEscolaridadServicio.ValidarInformacionRegistrarNivelEscolaridadAsync(registrarNivelEscolaridad);

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

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("GestionarEstadoNivelEscolaridad")]
        public async Task<IActionResult> GestionarEstadoNivelEscolaridad([FromBody] GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await nivelEscolaridadServicio.ValidarGestionarEstadoNivelEscolaridadAsync(gestionarEstadoNivelEscolaridad);

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

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionNivelEscolaridad")]
        public async Task<IActionResult> InformacionNivelEscolaridad()
        {
            try
            {
                var listaNivelEscolaridad = await nivelEscolaridadServicio.ValidarInformacionNivelEscolaridadAsync();
                return Ok(listaNivelEscolaridad);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionNivelEscolaridadEstadoActivo")]
        public async Task<IActionResult> InformacionNivelEscolaridadEstadoActivo()
        {
            try
            {
                var listaNivelEscolaridadEstadoActivo = await nivelEscolaridadServicio.ValidarInformacionNivelEscolaridadEstadoActivoAsync();
                return Ok(listaNivelEscolaridadEstadoActivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

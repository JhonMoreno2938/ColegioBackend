using Colegio.Modelos.Jornada.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Jornada")]
    public class JornadaControlador : ControllerBase
    {
        private readonly JornadaServicio jornadaServicio;

        public JornadaControlador(JornadaServicio jornadaServicio)
        {
            this.jornadaServicio = jornadaServicio;
        }


        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarJornada")]
        public async Task<IActionResult> RegistrarJornada([FromBody] RegistrarJornada registrarJornada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await jornadaServicio.ValidarInformacionRegistrarJornadaAsync(registrarJornada);

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
        [HttpPut("GestionarEstadoJornada")]
        public async Task<IActionResult> GestionarEstadoJornada([FromBody] GestionarEstadoJornada gestionarEstadoJornada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await jornadaServicio.ValidarGestionarEstadoJornadaAsync(gestionarEstadoJornada);

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
        [HttpGet("InformacionJornada")]
        public async Task<IActionResult> InformacionJornada()
        {
            try
            {
                var listaGrupo = await jornadaServicio.ValidarInformacionJornadaAsync();
                return Ok(listaGrupo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionJornadaEstadoActivo")]
        public async Task<IActionResult> InformacionJornadaEstadoActivo()
        {
            try
            {
                var listaGrupoEstadoActivo = await jornadaServicio.ValidarInformacionJornadaEstadoActivoAsync();
                return Ok(listaGrupoEstadoActivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

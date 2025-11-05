using Colegio.Modelos.Jornada_Sede.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "JornadaSede")]
    public class JornadaSedeControlador : ControllerBase
    {
        private readonly JornadaSedeServicio jornadaSedeServicio;

        public JornadaSedeControlador(JornadaSedeServicio jornadaSedeServicio)
        {
            this.jornadaSedeServicio = jornadaSedeServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarJornadaSede")]
        public async Task<IActionResult> RegistrarJornadaSede([FromBody] RegistrarJornadaSede registrarJornadaSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await jornadaSedeServicio.ValidarInformacionRegistrarJornadaSedeAsync(registrarJornadaSede);

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

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("GestionarEstadoJornadaSede")] 
        public async Task<IActionResult> GestionarEstadoJornadaSede([FromBody] GestionarEstadoJornadaSede gestionarEstadoJornadaSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await jornadaSedeServicio.ValidarGestionarEstadoJornadaSedeAsync(gestionarEstadoJornadaSede);

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
        [HttpGet("{codigoDaneSede}/Todas")] 
        public async Task<IActionResult> MostrarJornadaAsociadaSede([FromRoute] string codigoDaneSede)
        {
            if (string.IsNullOrWhiteSpace(codigoDaneSede))
            {
                return BadRequest(new { Mensaje = "El código DANE es requerido para la consulta." });
            }

            try
            {
                var resultadoLista = await jornadaSedeServicio.ValidarMostrarJornadaAsociadaSedeAsync(codigoDaneSede);

                return Ok(resultadoLista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("{codigoDaneSede}/Activas")] 
        public async Task<IActionResult> MostrarJornadaAsociadaSedeEstadoActivo([FromRoute] string codigoDaneSede)
        {
            if (string.IsNullOrWhiteSpace(codigoDaneSede))
            {
                return BadRequest(new { Mensaje = "El código DANE es requerido para la consulta." });
            }

            try
            {
                var resultadoLista = await jornadaSedeServicio.ValidarMostrarJornadaAsociadaSedeEstadoActivoAsync(codigoDaneSede);

                return Ok(resultadoLista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
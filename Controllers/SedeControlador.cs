using Colegio.Modelos.Sede.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Sede")]
    public class SedeControlador : ControllerBase
    {
        private readonly SedeServicio sedeServicio;

        public SedeControlador(SedeServicio sedeServicio)
        {
            this.sedeServicio = sedeServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarSede")]
        public async Task<IActionResult> RegistrarSede([FromBody] RegistrarSede registrarSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await sedeServicio.ValidarInformacionRegistrarSedeAsync(registrarSede);

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
        [HttpPut("ModificarInformacionSede")]
        public async Task<IActionResult> ModificarInformacionSede([FromBody] ModificarInformacionSede modificarInformacionSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await sedeServicio.ValidarModificarInformacionSedeAsync(modificarInformacionSede);

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
        [HttpPut("GestionarEstadoSede")]
        public async Task<IActionResult> GestionarEstadoSede([FromBody] GestionarEstadoSede gestionarEstadoSede)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await sedeServicio.ValidarGestionarEstadoSedeAsync(gestionarEstadoSede);

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
        [HttpGet("{codigoDaneSede}")] 
        public async Task<IActionResult> ConsultarSede([FromRoute] string codigoDaneSede)
        {
            if (string.IsNullOrWhiteSpace(codigoDaneSede))
            {
                return BadRequest(new { Mensaje = "El código DANE es requerido para la consulta." });
            }

            try
            {
                var resultado = await sedeServicio.ValidarConsultarSedeAsync(codigoDaneSede);

                if (resultado.exito)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Mensaje = $"Error en el servicio de datos: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionSede")]
        public async Task<IActionResult> InformacionSede()
        {
            try
            {
                var listaSede = await sedeServicio.ValidarInformacionSedeAsync();
                return Ok(listaSede);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionSedeEstadoActivo")]
        public async Task<IActionResult> InformacionSedeEstadoActivo()
        {
            try
            {
                var listaSedeEstadoActivo = await sedeServicio.ValidarInformacionSedeEstadoActivoAsync();
                return Ok(listaSedeEstadoActivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

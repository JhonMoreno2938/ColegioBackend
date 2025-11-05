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
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(RegistrarSede), new { Mensaje = resultado.mensaje });
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
                    // Se usa 200 OK para la modificación exitosa.
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
                    // Se usa 200 OK para la modificación exitosa.
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
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
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
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
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
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
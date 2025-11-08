using Colegio.Modelos.Porcentaje.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Porcentaje")]
    public class PorcentajeControlador : ControllerBase
    {
        private readonly PorcentajeServicio porcentajeServicio;

        public PorcentajeControlador(PorcentajeServicio porcentajeServicio)
        {
            this.porcentajeServicio = porcentajeServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarPorcentaje")]
        public async Task<IActionResult> RegistrarPorcentaje([FromBody] RegistrarPorcentaje registrarPorcentaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await porcentajeServicio.ValidarInformacionRegistrarPorcentajeAsync(registrarPorcentaje);

                if (resultado.exito)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(RegistrarPorcentaje), new { Mensaje = resultado.mensaje });
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
        [HttpPut("GestionarEstadoPorcentaje")]
        public async Task<IActionResult> GestionarEstadoPorcentaje([FromBody] GestionarEstadoPorcentaje gestionarEstadoPorcentaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await porcentajeServicio.ValidarInformacionGestionarEstadoPorcentajeAsync(gestionarEstadoPorcentaje);

                if (resultado.exito)
                {
                    // 200 OK para una modificación/actualización exitosa.
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
        [HttpGet("InformacionPorcentaje")]
        public async Task<IActionResult> InformacionPorcentaje()
        {
            try
            {
                var listaPorcentaje = await porcentajeServicio.ValidarInformacionPorcentajeAsync();
                return Ok(listaPorcentaje);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionPorcentajeEstadoActivo")]
        public async Task<IActionResult> InformacionGradoEstadoActivo()
        {
            try
            {
                var listaPorcentajeEstadoActivo = await porcentajeServicio.ValidarInformacionPorcentajeEstadoAsync();
                return Ok(listaPorcentajeEstadoActivo);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

    }
}

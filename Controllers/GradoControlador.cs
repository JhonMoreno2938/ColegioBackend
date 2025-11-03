using Colegio.Modelos.Grado.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Grado")]
    public class GradoControlador : ControllerBase
    {
        private readonly GradoServicio gradoServicio;

        public GradoControlador(GradoServicio gradoServicio)
        {
            this.gradoServicio = gradoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarGrado")]
        public async Task<IActionResult> RegistrarGrado([FromBody] RegistrarGrado registrarGrado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await gradoServicio.ValidarInformacionRegistrarGradoAsync(registrarGrado);

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
        [HttpPut("GestionarEstadoGrado")]
        public async Task<IActionResult> GestionarEstadoGrado([FromBody] GestionarEstadoGrado gestionarEstadoGrado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await gradoServicio.ValidarGestionarEstadoGradoAsync(gestionarEstadoGrado);

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
        [HttpGet("InformacionGrado")]
        public async Task<IActionResult> InformacionGrado()
        {
            try
            {
                var listaGrado = await gradoServicio.ValidarInformacionGradoAsync();
                return Ok(listaGrado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionGradoEstadoActivo")]
        public async Task<IActionResult> InformacionGradoEstadoActivo()
        {
            try
            {
                var listaGradoEstadoActivo = await gradoServicio.ValidarInformacionGradoEstadoActivoAsync();
                return Ok(listaGradoEstadoActivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

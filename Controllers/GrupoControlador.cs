using Colegio.Modelos.Grupo.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Grupo")]
    public class GrupoControlador : ControllerBase
    {
        private readonly GrupoServicio grupoServicio;

        public GrupoControlador(GrupoServicio grupoServicio)
        {
            this.grupoServicio = grupoServicio;
        }


        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarGrupo")]
        public async Task<IActionResult> RegistrarGrupo([FromBody] RegistrarGrupo registrarGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await grupoServicio.ValidarInformacionRegistrarGrupoAsync(registrarGrupo);

                if (resultado.exito)
                {
                    return CreatedAtAction(nameof(RegistrarGrupo), new { Mensaje = resultado.mensaje });
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
        [HttpPut("GestionarEstadoGrupo")]
        public async Task<IActionResult> GestionarEstadoGrupo([FromBody] GestionarEstadoGrupo gestionarEstadoGrupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await grupoServicio.ValidarGestionarEstadoGrupoAsync(gestionarEstadoGrupo);

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
        [HttpGet("InformacionGrupo")]
        public async Task<IActionResult> InformacionGrupo()
        {
            try
            {
                var listaGrupo = await grupoServicio.ValidarInformacionGrupoAsync();
                return Ok(listaGrupo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionGrupoEstadoActivo")]
        public async Task<IActionResult> InformacionGrupoEstadoActivo()
        {
            try
            {
                var listaGrupoEstadoActivo = await grupoServicio.ValidarInformacionGrupoEstadoActivoAsync();
                return Ok(listaGrupoEstadoActivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
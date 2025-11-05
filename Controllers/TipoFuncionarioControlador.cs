using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TipoFuncionario")]
    public class TipoFuncionarioControlador : ControllerBase
    {
        private readonly TipoFuncionarioServicio tipoFuncionarioServicio;

        public TipoFuncionarioControlador(TipoFuncionarioServicio tipoFuncionarioServicio)
        {
            this.tipoFuncionarioServicio = tipoFuncionarioServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionTipoFuncionario")] // Typo corregido
        public async Task<IActionResult> InformacionTipoFuncionario() // Typo corregido
        {
            try
            {
                var listaTipoFuncionario = await tipoFuncionarioServicio.ValidarInformacionTipoFuncionarioAsync();
                return Ok(listaTipoFuncionario);
            }
            catch (Exception ex)
            {
                // Se usa 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
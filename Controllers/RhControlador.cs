using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Rh")]
    public class RhControlador : ControllerBase
    {
        private readonly RhServicio rhServicio;

        public RhControlador(RhServicio rhServicio)
        {
            this.rhServicio = rhServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionRh")]
        public async Task<IActionResult> InformacionRh()
        {
            try
            {
                var listaRh = await rhServicio.ValidarInformacionRhAsync();
                return Ok(listaRh);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
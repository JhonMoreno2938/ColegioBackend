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
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Genero")]
    public class GeneroControlador : ControllerBase
    {
        private readonly GeneroServicio generoServicio;

        public GeneroControlador(GeneroServicio generoServicio)
        {
            this.generoServicio = generoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionGenero")]
        public async Task<IActionResult> InformacionGenero()
        {
            try
            {
                var listaGenero = await generoServicio.ValidarInformacionGeneroAsync();
                return Ok(listaGenero);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

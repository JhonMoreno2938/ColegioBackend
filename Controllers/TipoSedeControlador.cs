using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TipoSede")]
    public class TipoSedeControlador : ControllerBase
    {
        private readonly TipoSedeServicio tipoSedeServicio;

        public TipoSedeControlador(TipoSedeServicio tipoSedeServicio)
        {
            this.tipoSedeServicio = tipoSedeServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionTipoSede")]
        public async Task<IActionResult> InformacionTipoSede()
        {
            try
            {
                var listaTipoSede = await tipoSedeServicio.ValidarInformacionTipoSedeAsync();
                return Ok(listaTipoSede);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

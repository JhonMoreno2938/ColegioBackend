using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "TipoDocumento")]
    public class TipoDocumentoControlador : ControllerBase
    {
        private readonly TipoDocumentoServicio tipoDocumentoServicio;

        public TipoDocumentoControlador(TipoDocumentoServicio tipoDocumentoServicio)
        {
            this.tipoDocumentoServicio = tipoDocumentoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionTipoDocumento")]
        public async Task<IActionResult> InformacionTipoDocumento()
        {
            try
            {
                var listaTipoDocumento = await tipoDocumentoServicio.ValidarInformacionTipoDocumentoAsync();
                return Ok(listaTipoDocumento);
            }
            catch (Exception ex)
            {
                // Se usa 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
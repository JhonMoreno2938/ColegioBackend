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
        [HttpGet("InformacionTipoFuncioanrio")]
        public async Task<IActionResult> InformacionTipoFuncioanrio()
        {
            try
            {
                var listaTipoFuncionario = await tipoFuncionarioServicio.ValidarInformacionTipoFuncionarioAsync();
                return Ok(listaTipoFuncionario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}

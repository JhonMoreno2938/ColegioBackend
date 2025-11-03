using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RolUsuario")]
    public class RolUsuarioControlador : ControllerBase
    {
        private readonly RolUsuarioServicio rolUsuarioServicio;

        public RolUsuarioControlador(RolUsuarioServicio rolUsuarioServicio)
        {
            this.rolUsuarioServicio = rolUsuarioServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionRolUsuario")]
        public async Task<IActionResult> InformacionRolUsuario()
        {
            try
            {
                var listaRolusuario = await rolUsuarioServicio.ValidarInformacionRolUsuarioAsync();
                return Ok(listaRolusuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

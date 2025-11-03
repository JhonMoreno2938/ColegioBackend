using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Departamento")]
    public class DepartamentoControlador : ControllerBase
    {
        private readonly DepartamentoServicio departamentoServicio;

        public DepartamentoControlador(DepartamentoServicio departamentoServicio)
        {
            this.departamentoServicio = departamentoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionDepartamento")]
        public async Task<IActionResult> InformacionDepartamento()
        {
            try
            {
                var listaDepartamento = await departamentoServicio.ValidarInformacionDepartamentoAsync();
                return Ok(listaDepartamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

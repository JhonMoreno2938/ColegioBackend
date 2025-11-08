using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "NombrePeriodoAcademico")]
    public class NombrePeriodoAcademicoControlador : ControllerBase
    {
        private readonly NombrePeriodoAcademicoServicio nombrePeriodoAcademicoServicio;

        public NombrePeriodoAcademicoControlador(NombrePeriodoAcademicoServicio nombrePeriodoAcademicoServicio)
        {
            this.nombrePeriodoAcademicoServicio = nombrePeriodoAcademicoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionNombrePeriodoAcademico")]
        public async Task<IActionResult> InformacionNombrePeriodoAcademico()
        {
            try
            {
                var listaNombrePeriodoAcademico = await nombrePeriodoAcademicoServicio.ValidarInformacionNombrePeriodoAcademicoAsync();
                return Ok(listaNombrePeriodoAcademico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}

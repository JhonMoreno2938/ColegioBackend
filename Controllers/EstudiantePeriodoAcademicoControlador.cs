using Colegio.Modelos.Estudiante_Periodo_Academico.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "EstudiantePeriodoAcademico")]
    public class EstudiantePeriodoAcademicoControlador : ControllerBase
    {
        private readonly EstudiantePeriodoAcademicoServicio estudiantePeriodoAcademicoServicio;

        public EstudiantePeriodoAcademicoControlador(EstudiantePeriodoAcademicoServicio estudiantePeriodoAcademicoServicio)
        {
            this.estudiantePeriodoAcademicoServicio = estudiantePeriodoAcademicoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("PrematricularEstudiantePeriodoAcademico")]
        public async Task<IActionResult> PrematricularEstudiantePeriodoAcademico([FromBody] PrematricularEstudiantePeriodoAcademico prematricularEstudiantePeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await estudiantePeriodoAcademicoServicio.ValidarPrematricularEstudiantePeriodoAcademico(prematricularEstudiantePeriodoAcademico);

                if (resultado.exitoso)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(PrematricularEstudiantePeriodoAcademico), new { Mensaje = resultado.mensaje });
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}

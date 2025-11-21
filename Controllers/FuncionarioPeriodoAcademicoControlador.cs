using Colegio.Modelos.Funcionario_Periodo_Academico.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FuncionarioPeriodoAcademico")]
    public class FuncionarioPeriodoAcademicoControlador : ControllerBase
    {
        private readonly FuncionarioPeriodoAcademicoServicio funcionarioPeriodoAcademicoServicio;

        public FuncionarioPeriodoAcademicoControlador(FuncionarioPeriodoAcademicoServicio funcionarioPeriodoAcademicoServicio)
        {
            this.funcionarioPeriodoAcademicoServicio = funcionarioPeriodoAcademicoServicio;
        }


        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("MatricularFuncionariosPeriodoAcademico")]
        public async Task<IActionResult> MatricularFuncionariosPeriodoAcademico([FromBody] MatricularFuncionariosPeriodoAcademico matricularFuncionariosPeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioPeriodoAcademicoServicio.ValidarMatricularFuncionariosPeriodoAcademico(matricularFuncionariosPeriodoAcademico);

                if (resultado.exitoso)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(MatricularFuncionariosPeriodoAcademico), new { Mensaje = resultado.mensaje });
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

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("MatricularFuncionarioPeriodoAcademico")]
        public async Task<IActionResult> MatricularFuncionarioPeriodoAcademico([FromBody] MatricularFuncionarioPeriodoAcademico matricularFuncionarioPeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioPeriodoAcademicoServicio.ValidarMatricularFuncionarioPeriodoAcademico(matricularFuncionarioPeriodoAcademico);

                if (resultado.exitoso)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(MatricularFuncionarioPeriodoAcademico), new { Mensaje = resultado.mensaje });
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

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("ActualizarFechaHabilitacionFuncionarioPeriodoAcademico")]
        public async Task<IActionResult> ActualizarFechaHabilitacionFuncionarioPeriodoAcademico([FromBody] ActualizarFechaHabilitacionFuncionarioPeriodoAcademico actualizarFechaHabilitacionFuncionarioPeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioPeriodoAcademicoServicio.ValidarActualizarFechaHabilitacionFuncionarioPeriodoAcademico(actualizarFechaHabilitacionFuncionarioPeriodoAcademico);

                if (resultado.exitoso)
                {
                    return Ok(new { Mensaje = resultado.mensaje });
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

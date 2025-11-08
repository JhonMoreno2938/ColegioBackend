using Colegio.Modelos.Periodo_Academico.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "PeriodoAcademico")]
    public class PeriodoAcademicoControlador : ControllerBase
    {
        private readonly PeriodoAcademicoServicio periodoAcademicoServicio;

        public PeriodoAcademicoControlador(PeriodoAcademicoServicio periodoAcademicoServicio)
        {
            this.periodoAcademicoServicio = periodoAcademicoServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarPeriodoAcademico")]
        public async Task<IActionResult> RegistrarPeriodoAcademico([FromBody] RegistrarPeriodoAcademico registrarPeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await periodoAcademicoServicio.ValidarInformacionRegistrarPeriodoAcademicoAsync(registrarPeriodoAcademico);

                if (resultado.exito)
                {
                    // Se usa 201 Created para la creación exitosa.
                    return CreatedAtAction(nameof(RegistrarPeriodoAcademico), new { Mensaje = resultado.mensaje });
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
        [HttpGet("{idPeriodoAcademico}")]
        public async Task<IActionResult> ConsultarPeriodoAcademico([FromRoute] int idPeriodoAcademico)
        {
            if (idPeriodoAcademico == 0)
            {
                return BadRequest(new { Mensaje = "El id del periodo academico es obligatorio." });
            }

            try
            {
                var resultado = await periodoAcademicoServicio.ValidarConsultarPeriodoAcademicoAsync(idPeriodoAcademico);

                if (resultado.exito)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("ModificarPeriodoAcademico")]
        public async Task<IActionResult> ModificarPeriodoAcademico([FromBody] ModificarPeriodoAcademico modificarPeriodoAcademico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await periodoAcademicoServicio.ValidarModificarPeriodoAcademicoAsync(modificarPeriodoAcademico);

                if (resultado.exito)
                {
                    // Se usa 200 OK para la modificación exitosa.
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

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionNombrePeriodoAcademico")]
        public async Task<IActionResult> InformacionNombrePeriodoAcademico()
        {
            try
            {
                var listaNombrePeriodoAcademico = await periodoAcademicoServicio.ValidarInformacionNombrePeriodoAcademicoAsync();
                return Ok(listaNombrePeriodoAcademico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionAnnioPeriodoAcademico")]
        public async Task<IActionResult> InformacionAnnioPeriodoAcademico()
        {
            try
            {
                var listaAnnioPeriodoAcademico = await periodoAcademicoServicio.ValidarInformacionAnnioPeriodoAcademicoAsync();
                return Ok(listaAnnioPeriodoAcademico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("{annio}/annio")]
        public async Task<IActionResult> ObtenerPeriodoAcademico([FromRoute] int annio)
        {
            if (annio == 0)
            {
                return BadRequest(new { Mensaje = "El año es obligatorio." });
            }

            try
            {
                var resultadoLista = await periodoAcademicoServicio.ValidarObtenerPeriodoAcademicoAsync(annio);

                return Ok(resultadoLista);
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error para fallas de servidor/DB.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }


}

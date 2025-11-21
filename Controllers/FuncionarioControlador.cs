using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Funcionario")]
    public class FuncionarioControlador : ControllerBase
    {
        private readonly FuncionarioServicio funcionarioServicio;

        public FuncionarioControlador(FuncionarioServicio funcionarioServicio)
        {
            this.funcionarioServicio = funcionarioServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarFuncionario")]
        public async Task<IActionResult> RegistrarFuncionario([FromBody] RegistrarFuncionario registrarFuncionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioServicio.ValidarInformacionRegistrarFuncionarioAsync(registrarFuncionario);

                if (resultado.exito)
                {
                    return CreatedAtAction(nameof(RegistrarFuncionario), new { Id = resultado.mensajeId, Mensaje = resultado.mensaje });
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
        [HttpPut("ModificarInformacionFuncionario")]
        public async Task<IActionResult> ModificarInformacionFuncionario([FromBody] ModificarInformacionFuncionario modificarInformacionFuncionario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioServicio.ValidarModificarInformacionFuncionarioAsync(modificarInformacionFuncionario);

                if (resultado.exitoso)
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

        [Authorize(Policy = "SecretarioODocente")]
        [HttpGet("{numeroDocumento}/ConsultarFuncionario")]
        public async Task<IActionResult> ConsultarFuncioanrio([FromRoute] string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                return BadRequest(new { Mensaje = "El número de documento es requerido para la consulta." });
            }

            try
            {
                var resultado = await funcionarioServicio.ValidarConsultarFuncionarioAsync(numeroDocumento);

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
        [HttpGet("InformacionFuncionario")]
        public async Task<IActionResult> InformacionFuncionario()
        {
            try
            {
                var listaFuncionario = await funcionarioServicio.ValidarInformacionFuncionarioAsync();
                return Ok(listaFuncionario);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloDocente")]
        [HttpGet("{nombreUsuario}/ConsultarGradoGrupoFuncionarioEstadoActivo")]
        public async Task<IActionResult> ConsultarGradoGrupoFuncionarioEstadoActivo([FromRoute] string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return BadRequest(new { Mensaje = "El nombre de usuario es requerido para la consulta." });
            }

            try
            {
                var informacionGradoGrupoFuncionarioEstadoActivo = await funcionarioServicio.ValidarInformacionGradoGrupoFuncionarioEstadoActivoAsync(nombreUsuario);
                return Ok(informacionGradoGrupoFuncionarioEstadoActivo);
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloDocente")]
        [HttpGet("{nombreUsuario}/ConsultarCompetenciaFuncionario")]
        public async Task<IActionResult> ConsultarCompetenciaFuncionario([FromRoute] string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return BadRequest(new { Mensaje = "El nombre de usuario es requerido para la consulta." });
            }

            try
            {
                var informacionCompetenciaFuncionario = await funcionarioServicio.ValidarConsultarCompetenciaFuncionarioAsync(nombreUsuario);
                return Ok(informacionCompetenciaFuncionario);
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }
    }
}
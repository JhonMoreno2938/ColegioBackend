using Colegio.Modelos.Funcionario_Asignatura.Procedimientos;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FuncionarioAsignatura")]
    public class FuncionarioAsignaturaControlador : ControllerBase
    {
        private readonly FuncionarioAsignaturaServicio funcionarioAsignaturaServicio;

        public FuncionarioAsignaturaControlador(FuncionarioAsignaturaServicio funcionarioAsignaturaServicio)
        {
            this.funcionarioAsignaturaServicio = funcionarioAsignaturaServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("AsignarFuncionarioAsignatura")]
        public async Task<IActionResult> AsignarFuncionarioAsignatura([FromBody] AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 🛑 Llama al método del servicio que devuelve 'bool'
                var resultado = await funcionarioAsignaturaServicio.ValidarInformacionAsignarFuncionarioAsignaturaAsync(asignarFuncionarioAsignatura);

                if (resultado == true)
                {
                    // ✅ Respuesta 200 OK: Devuelve un JSON que solo indica éxito.
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                    });
                }
            }
            catch (Exception ex)
            {
                // 500 Internal Server Error (Fallo del servidor/conexión)
                return StatusCode(500, new { Mensaje = $"Error interno del servidor." });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("GestioanrEstadoFuncionarioAsignatura")]
        public async Task<IActionResult> GestioanrEstadoFuncionarioAsignatura([FromBody] GestionarEstadoFuncionarioAsignatura gestionarEstadoFuncionarioAsignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await funcionarioAsignaturaServicio.ValidarGestionarEstadoFuncionarioAsignaturaAsync(gestionarEstadoFuncionarioAsignatura);

                if (resultado.exitoso)
                {
                    // Se usa 200 OK para la modificación.
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

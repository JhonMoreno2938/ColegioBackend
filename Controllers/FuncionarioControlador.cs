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
    }
}
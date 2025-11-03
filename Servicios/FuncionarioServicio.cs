using Colegio.Interfaz;
using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using System.Threading.Tasks;

namespace Colegio.Servicios
{
    public class FuncionarioServicio
    {
        private readonly IFuncionario funcionario;

        public FuncionarioServicio(IFuncionario funcionario)
        {
            this.funcionario = funcionario;
        }

        public async Task<SalidaRegistrarFuncionario> ValidarInformacionRegistrarFuncionarioAsync(RegistrarFuncionario registrarFuncionario)
        {
            SalidaRegistrarFuncionario resultado = await funcionario.RegistrarFuncionarioAsync(registrarFuncionario);

            return resultado;
        }

    }
}

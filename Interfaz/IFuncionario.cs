using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IFuncionario
    {
        Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(RegistrarFuncionario registrarFuncionario);
    }
}

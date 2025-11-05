using Colegio.Modelos.Funcionario;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IFuncionario
    {
        Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(FuncionarioModelo funcionarioModelo);
    }
}

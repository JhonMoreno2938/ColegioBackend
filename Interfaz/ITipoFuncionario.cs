using Colegio.Modelos.Tipo_Funcionario;

namespace Colegio.Interfaz
{
    public interface ITipoFuncionario
    {
        Task<List<TipoFuncionarioModelo>> InformacionTipoFuncionarioAsync();
    }
}

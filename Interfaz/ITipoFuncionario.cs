using Colegio.Modelos.Tipo_Funcionario.Vistas;

namespace Colegio.Interfaz
{
    public interface ITipoFuncionario
    {
        Task<List<ListarTipoFuncionario>> InformacionTipoFuncionarioAsync();
    }
}

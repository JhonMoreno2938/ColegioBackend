using Colegio.Modelos.Funcionario;
using Colegio.Modelos.Funcionario.Consultas;
using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using Colegio.Modelos.Funcionario.Vistas;
using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface IFuncionario
    {
        Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(FuncionarioModelo funcionarioModelo);
        Task<ResultadoOperacion> ModificarInformacionFuncionarioAsync(FuncionarioModelo funcionarioModelo);
        Task<ConsultarFuncionario> ConsultarFuncionarioAsync(string numeroDocumento);
        Task<List<ListarFuncionario>> InformacionFuncionarioAsync();
        Task<List<ConsultarGradoGrupoFuncionarioEstadoActivo>> InformacionGradoGrupoFuncionarioEstadoActivoAsync(string nombreUsuario);
        Task<List<ConsultarCompetenciaFuncionario>> ConsultarCompetenciaFuncionarioAsync(string nombreUsuario);
    }
}

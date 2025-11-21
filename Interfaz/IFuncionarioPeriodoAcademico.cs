using Colegio.Modelos.Funcionario_Periodo_Academico;
using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface IFuncionarioPeriodoAcademico
    {
        Task<ResultadoOperacion> MatricularFuncionariosPeriodoAcademicoAsync(FuncionarioPeriodoAcademicoModelo funcionarioPeriodoAcademicoModelo);
        Task<ResultadoOperacion> MatricularFuncionarioPeriodoAcademicoAsync(string numeroDocumento, string fechaInicioHabilitacion, string fechaFinalHabilitacion, int idPeriodoAcademico);
        Task<ResultadoOperacion> ActualizarFechaHabiltiacionFuncionarioPeriodoAcademicoAsync(string listaNumeroDocumento, string fechaInicioHabilitacion, string fechaFinalHabilitacion, int idPeriodoAcademico);
    }
}

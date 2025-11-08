using Colegio.Modelos.Periodo_Academico.Salidas_Procedimientos;
using Colegio.Modelos.Periodo_Academico.Vistas;

namespace Colegio.Interfaz
{
    public interface IPeriodoAcademico
    {
        Task<ResultadoMensajePeriodoAcademico> RegistrarPeriodoAcademicoAsync(string nombrePeriodoAcademico, int valorPorcentaje, string fechaInicioPeriodoAcademico, string fechaFinalPeriodoAcademico);
        Task<SalidaConsultarPeriodoAcademico> ConsultarPeriodoAcademicoAsync(int idPeriodoAcademico);
        Task<ResultadoMensajePeriodoAcademico> ModificarPeriodoAcademico(int idPeriodoAcademico, string fechaInicioPeriodoAcademico, string fechaFinalPeriodoAcademico, int valorPorcentaje);
        Task<List<ListarNombrePeriodoAcademico>> InformacionNombrePeriodoAcademico();
        Task<List<ListarAnnioPeriodoAcademico>> InformacionAnnioPeriodoAcademico();
        Task<List<SalidaObtenerPeriodoAcademicoAnnio>> ObtenerPeriodoAcademicoAsync(int annio);
    }
}

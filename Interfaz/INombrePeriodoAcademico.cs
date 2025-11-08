using Colegio.Modelos.Nombre_Periodo_Academico;

namespace Colegio.Interfaz
{
    public interface INombrePeriodoAcademico
    {
        Task<List<NombrePeriodoAcademicoModelo>> InformacionPeriodoAcademicoAsync();
    }
}

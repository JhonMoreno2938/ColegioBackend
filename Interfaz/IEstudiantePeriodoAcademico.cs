using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface IEstudiantePeriodoAcademico
    {
        Task<ResultadoOperacion> PrematricularEstudiantePeriodoAcademico(string numeroDocumento, string nombrePeriodoAcademico);
    }
}

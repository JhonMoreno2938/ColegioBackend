using Colegio.Modelos.Competencia;
using Colegio.Modelos.Competencia.Procedimientos;
using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface ICompetencia
    {
        Task<ResultadoOperacion> RegistrarCompetenciaAsync(CompetenciaModelo competenciaModelo);
        Task<List<ConsultarCompetenciaAsociadaGradoGrupoSalida>> InformacionCompetenciaAsociadaGradoGrupoAsync(CompetenciaModelo competenciaModelo);
        Task<ResultadoOperacion> ModificarDescripcionCompetenciaAsync(CompetenciaModelo competenciaModelo);
        Task<ResultadoOperacion> EliminarCompetenciaAsync(CompetenciaModelo competenciaModelo);

    }
}

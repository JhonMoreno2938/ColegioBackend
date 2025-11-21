namespace Colegio.Interfaz
{
    public interface ICompetenciaEstudiante
    {
        Task<bool> CalificarCompetenciaAsync(int idCompetencia, string numeroDocumento, string estadoCompetenciaEstudiante, int idFuncionarioAsignaturaGradoGrupo);
    }
}

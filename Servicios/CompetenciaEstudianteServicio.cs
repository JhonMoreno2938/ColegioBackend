using Colegio.Interfaz;
using Colegio.Modelos.Competencia_Estudiante.Procedimientos;

namespace Colegio.Servicios
{
    public class CompetenciaEstudianteServicio
    {
        private readonly ICompetenciaEstudiante competenciaEstudiante;

        public CompetenciaEstudianteServicio(ICompetenciaEstudiante competenciaEstudiante)
        {
            this.competenciaEstudiante = competenciaEstudiante;
        }

        public async Task<bool> ValidarCalificarCompetenciaAsync(CalificarCompetencia calificarCompetencia)
        {
            bool resultado = await competenciaEstudiante.CalificarCompetenciaAsync(
                calificarCompetencia.idCompetencia,
                calificarCompetencia.numeroDocumento,
                calificarCompetencia.estadoCompetenciaEstudiante,
                calificarCompetencia.idFuncionarioAsignaturaGradoGrupo
                );

            return resultado;
        }
    }
}

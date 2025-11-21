using Colegio.Interfaz;
using Colegio.Modelos.Competencia;
using Colegio.Modelos.Competencia.Procedimientos;
using Colegio.Modelos.Funcionario_Asignatura_Grado_Grupo;
using Colegio.Modelos.Periodo_Academico;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class CompetenciaServicio
    {
        private readonly ICompetencia competencia;

        public CompetenciaServicio(ICompetencia competencia)
        {
            this.competencia = competencia;
        }

        private CompetenciaModelo MapearRegistrarCompetencia(RegistrarCompetencia registrarCompetencia)
        {
            var funcionarioAsignaturaGradoGrupo = new FuncionarioAsignaturaGradoGrupoModelo
            {
                pkIdFuncionarioAsignaturaGradoGrupo = registrarCompetencia.idFuncionarioAsignaturaGradoGrupo
            };

            var periodoAcademico = new PeriodoAcademicoModelo
            {
                pkIdPeriodoAcademico = registrarCompetencia.idPeriodoAcademico
            };

            return new CompetenciaModelo
            {
                descripcionCompetencia = registrarCompetencia.descripcionCompetencia,
                funcionarioAsignaturaGradoGrupoModelo = funcionarioAsignaturaGradoGrupo,
                periodoAcademicoModelo = periodoAcademico
            };
        }

        private CompetenciaModelo MapearListarCompetencia(ConsultarCompetenciaAsociadaGradoGrupoEntrada consultarCompetenciaAsociadaGradoGrupoEntrada)
        {
            var funcionarioAsignaturaGradoGrupo = new FuncionarioAsignaturaGradoGrupoModelo
            {
                pkIdFuncionarioAsignaturaGradoGrupo = consultarCompetenciaAsociadaGradoGrupoEntrada.idFuncionarioAsignaturaGradoGrupo
            };

            var periodoAcademico = new PeriodoAcademicoModelo
            {
                pkIdPeriodoAcademico = consultarCompetenciaAsociadaGradoGrupoEntrada.idPeriodoAcademico
            };

            return new CompetenciaModelo
            {
                funcionarioAsignaturaGradoGrupoModelo = funcionarioAsignaturaGradoGrupo,
                periodoAcademicoModelo = periodoAcademico
            };
        }

        private CompetenciaModelo MapearModificarDescripcionCompetencia(ModificarDescripcionCompetencia modificarDescripcionCompetencia)
        {
            return new CompetenciaModelo
            {
                pkIdCompetencia = modificarDescripcionCompetencia.idCompetencia,
                descripcionCompetencia = modificarDescripcionCompetencia.descripcionCompetencia
            };
        }

        private CompetenciaModelo MapearEliminarCompetencia(int idCompetencia)
        {
            return new CompetenciaModelo
            {
                pkIdCompetencia = idCompetencia
            };
        }

        public async Task<ResultadoOperacion> ValidarRegistrarCompetenciaAsync(RegistrarCompetencia registrarCompetencia)
        {
            CompetenciaModelo competenciaModelo = MapearRegistrarCompetencia(registrarCompetencia);

            ResultadoOperacion resultadoOperacion = await competencia.RegistrarCompetenciaAsync(competenciaModelo);

            return resultadoOperacion;
        }

        public async Task<List<ConsultarCompetenciaAsociadaGradoGrupoSalida>> ValidarInformacionCompetenciaAsociadaGradoGrupoAsync(ConsultarCompetenciaAsociadaGradoGrupoEntrada consultarCompetenciaAsociadaGradoGrupoEntrada)
        {
            CompetenciaModelo competenciaModelo = MapearListarCompetencia(consultarCompetenciaAsociadaGradoGrupoEntrada);

            List<ConsultarCompetenciaAsociadaGradoGrupoSalida> listarCompetencia = await competencia.InformacionCompetenciaAsociadaGradoGrupoAsync(competenciaModelo);

            return listarCompetencia;
        }

        public async Task<ResultadoOperacion> ValidarModificarDescripcionCompetenciaAsync(ModificarDescripcionCompetencia modificarDescripcionCompetencia)
        {
            CompetenciaModelo competenciaModelo = MapearModificarDescripcionCompetencia(modificarDescripcionCompetencia);

            ResultadoOperacion resultadoOperacion = await competencia.ModificarDescripcionCompetenciaAsync(competenciaModelo);

            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion> ValidarEliminarCompetenciaAsync(int idCompetencia)
        {
            CompetenciaModelo competenciaModelo = MapearEliminarCompetencia(idCompetencia);

            ResultadoOperacion resultadoOperacion = await competencia.EliminarCompetenciaAsync(competenciaModelo);

            return resultadoOperacion;
        }

    }
}

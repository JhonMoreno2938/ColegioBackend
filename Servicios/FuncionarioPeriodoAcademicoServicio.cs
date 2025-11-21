using Colegio.Interfaz;
using Colegio.Modelos.Funcionario_Periodo_Academico;
using Colegio.Modelos.Funcionario_Periodo_Academico.Procedimientos;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class FuncionarioPeriodoAcademicoServicio
    {
        private readonly IFuncionarioPeriodoAcademico funcionarioPeriodoAcademico;

        public FuncionarioPeriodoAcademicoServicio(IFuncionarioPeriodoAcademico funcionarioPeriodoAcademico)
        {
            this.funcionarioPeriodoAcademico = funcionarioPeriodoAcademico;
        }


        private FuncionarioPeriodoAcademicoModelo MapearMatricularFuncionariosPeriodoAcademico(MatricularFuncionariosPeriodoAcademico matricularFuncionariosPeriodoAcademico)
        {

            return new FuncionarioPeriodoAcademicoModelo
            {
                fechaInicioHabilitacion = matricularFuncionariosPeriodoAcademico.fechaInicioHabilitacion,
                fechaFinalHabilitacion = matricularFuncionariosPeriodoAcademico.fechaFinalHabilitacion
            };
        }

        private string ConstruirListaNumeroDocumento(ActualizarFechaHabilitacionFuncionarioPeriodoAcademico actualizarFuncionarioPeriodoAcademico)
        {
            IEnumerable<string> numeroDocumento = actualizarFuncionarioPeriodoAcademico.listaNumeroDocumento.Select(nd => nd.numeroDocumento);
            return string.Join(",", numeroDocumento);
        }

        public async Task<ResultadoOperacion> ValidarMatricularFuncionariosPeriodoAcademico(MatricularFuncionariosPeriodoAcademico matricularFuncionariosPeriodoAcademico)
        {
            FuncionarioPeriodoAcademicoModelo funcionarioPeriodoAcademicoModelo = MapearMatricularFuncionariosPeriodoAcademico(matricularFuncionariosPeriodoAcademico);

            ResultadoOperacion resultadoOperacion = await funcionarioPeriodoAcademico.MatricularFuncionariosPeriodoAcademicoAsync(funcionarioPeriodoAcademicoModelo);

            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion> ValidarMatricularFuncionarioPeriodoAcademico(MatricularFuncionarioPeriodoAcademico matricularFuncionarioPeriodoAcademico)
        {
            ResultadoOperacion resultadoOperacion = await funcionarioPeriodoAcademico.MatricularFuncionarioPeriodoAcademicoAsync(
                matricularFuncionarioPeriodoAcademico.numeroDocumento,
                matricularFuncionarioPeriodoAcademico.fechaInicioHabilitacion,
                matricularFuncionarioPeriodoAcademico.fechaFinalHabilitacion,
                matricularFuncionarioPeriodoAcademico.idPeriodoAcademico
                );

            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion> ValidarActualizarFechaHabilitacionFuncionarioPeriodoAcademico(ActualizarFechaHabilitacionFuncionarioPeriodoAcademico actualizarFuncionarioPeriodoAcademico)
        {
            string listaNumeroDocumento = ConstruirListaNumeroDocumento(actualizarFuncionarioPeriodoAcademico);

            ResultadoOperacion resultadoOperacion = await funcionarioPeriodoAcademico.ActualizarFechaHabiltiacionFuncionarioPeriodoAcademicoAsync(
                listaNumeroDocumento,
                actualizarFuncionarioPeriodoAcademico.fechaInicioHabilitacion,
                actualizarFuncionarioPeriodoAcademico.fechaFinalHabilitacion,
                actualizarFuncionarioPeriodoAcademico.idPeriodoAcademico
                );

            return resultadoOperacion;
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Periodo_Academico.Procedimientos;
using Colegio.Modelos.Periodo_Academico.Salidas_Procedimientos;
using Colegio.Modelos.Periodo_Academico.Vistas;

namespace Colegio.Servicios
{
    public class PeriodoAcademicoServicio
    {
        private readonly IPeriodoAcademico periodoAcademico;

        public PeriodoAcademicoServicio(IPeriodoAcademico periodoAcademico)
        {
            this.periodoAcademico = periodoAcademico;
        }

        public async Task<ResultadoMensajePeriodoAcademico> ValidarInformacionRegistrarPeriodoAcademicoAsync(RegistrarPeriodoAcademico registrarPeriodoAcademico)
        {
            ResultadoMensajePeriodoAcademico resultado = await periodoAcademico.RegistrarPeriodoAcademicoAsync(
                registrarPeriodoAcademico.nombrePeriodoAcademico,
                registrarPeriodoAcademico.valorPorcentaje,
                registrarPeriodoAcademico.fechaInicioPeriodoAcademico,
                registrarPeriodoAcademico.fechaFinalPeriodoAcademico
                );

            return resultado;
        }

        public async Task<SalidaConsultarPeriodoAcademico> ValidarConsultarPeriodoAcademicoAsync(int idPeriodoAcademico)
        {
            SalidaConsultarPeriodoAcademico resultado = await periodoAcademico.ConsultarPeriodoAcademicoAsync(idPeriodoAcademico);

            if (resultado == null)
            {
                return new SalidaConsultarPeriodoAcademico
                {
                    exito = false,
                    mensaje = $"El período académico con ID {idPeriodoAcademico} no fue encontrado."
                };
            }

            resultado.exito = true;

            return resultado;
        }

        public async Task<ResultadoMensajePeriodoAcademico> ValidarModificarPeriodoAcademicoAsync(ModificarPeriodoAcademico modificarPeriodoAcademico)
        {
            ResultadoMensajePeriodoAcademico resultado = await periodoAcademico.ModificarPeriodoAcademico(
                modificarPeriodoAcademico.idPeriodoAcademico,
                modificarPeriodoAcademico.fechaInicioPeriodoAcademico,
                modificarPeriodoAcademico.fechaFinalPeriodoAcademico,
                modificarPeriodoAcademico.valorPorcentaje
                );

            return resultado;
        }

        public async Task<List<ListarNombrePeriodoAcademico>> ValidarInformacionNombrePeriodoAcademicoAsync()
        {
            List<ListarNombrePeriodoAcademico> listaNombrePeriodoAcademico = await periodoAcademico.InformacionNombrePeriodoAcademico();

            if (listaNombrePeriodoAcademico == null)
            {
                return new List<ListarNombrePeriodoAcademico>();
            }

            return listaNombrePeriodoAcademico;
        }

        public async Task<List<ListarAnnioPeriodoAcademico>> ValidarInformacionAnnioPeriodoAcademicoAsync()
        {
            List<ListarAnnioPeriodoAcademico> listaAnnioPeriodoAcademico = await periodoAcademico.InformacionAnnioPeriodoAcademico();

            if(listaAnnioPeriodoAcademico == null)
            {
                return new List<ListarAnnioPeriodoAcademico>();
            }

            return listaAnnioPeriodoAcademico;
        }

        public async Task<List<SalidaObtenerPeriodoAcademicoAnnio>> ValidarObtenerPeriodoAcademicoAsync(int annio)
        {

            List<SalidaObtenerPeriodoAcademicoAnnio> listaPeriodoAcademico = await periodoAcademico.ObtenerPeriodoAcademicoAsync(annio);

            if (listaPeriodoAcademico == null)
            {
                return new List<SalidaObtenerPeriodoAcademicoAnnio>();
            }
            
            return listaPeriodoAcademico;
        }
    }
}

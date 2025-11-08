using Colegio.Interfaz;
using Colegio.Modelos.Nombre_Periodo_Academico;
using Colegio.Modelos.Nombre_Periodo_Academico.Vistas;

namespace Colegio.Servicios
{
    public class NombrePeriodoAcademicoServicio
    {
        private readonly INombrePeriodoAcademico nombrePeriodoAcademico;

        public NombrePeriodoAcademicoServicio(INombrePeriodoAcademico nombrePeriodoAcademico)
        {
            this.nombrePeriodoAcademico = nombrePeriodoAcademico;
        }

        private List<ListarNombrePeriodoAcademico> MapearListarNombrePeriodoAcademico(List<NombrePeriodoAcademicoModelo> nombrePeriodoAcademicoModelo)
        {
            return nombrePeriodoAcademicoModelo.Select(modelo => new ListarNombrePeriodoAcademico
            {
                nombrePeriodoAcademico = modelo.nombrePeriodoAcademico
            }).ToList();
        }

        public async Task<List<ListarNombrePeriodoAcademico>> ValidarInformacionNombrePeriodoAcademicoAsync()
        {
            var modeloNombrePeriodoAcademico = await nombrePeriodoAcademico.InformacionPeriodoAcademicoAsync();

            var resultado = MapearListarNombrePeriodoAcademico(modeloNombrePeriodoAcademico);

            return resultado;
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Porcentaje;
using Colegio.Modelos.Tipo_Calificacion_Academica;
using Colegio.Modelos.Tipo_Calificacion_Academica.Procedimientos;
using Colegio.Modelos.Tipo_Calificacion_Academica.Vistas;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class TipoCalificacionAcademicaServicio
    {
        private readonly ITipoCalificacionAcademica tipoCalificacionAcademica;

        public TipoCalificacionAcademicaServicio(ITipoCalificacionAcademica tipoCalificacionAcademica)
        {
            this.tipoCalificacionAcademica = tipoCalificacionAcademica;
        }

        private TipoCalificacionAcademicaModelo MapearRegistrarTipoCalificacionAcademica(RegistrarTipoCalificacionAcademica registrarTipoCalificacionAcademica)
        {

            var porcentaje = new PorcentajeModelo()
            {
                valorPorcentaje = registrarTipoCalificacionAcademica.valorPorcentaje
            };

            return new TipoCalificacionAcademicaModelo
            {

                nombreTipoCalificacionAcademica = registrarTipoCalificacionAcademica.nombreTipoCalificacionAcademica,

                porcentajeModelo = porcentaje
            };

        }

        private List<ListarTipoCalificacionAcademica> MapearListarTipoCalificacionAcademica(List<TipoCalificacionAcademicaModelo> tipoCalificacionAcademicaModelo)
        {

            return tipoCalificacionAcademicaModelo.Select(modelo => new ListarTipoCalificacionAcademica
            {
               nombreTipoCalificacionAcademica = modelo.nombreTipoCalificacionAcademica,
               valorPorcentaje = modelo.porcentajeModelo.valorPorcentaje
            }).ToList();

        }

        private TipoCalificacionAcademicaModelo MapearModificarTipoCalificacionAcademica(ModificarTipoCalificacionAcademica modificarTipoCalificacionAcademica)
        {
            var porcentaje = new PorcentajeModelo()
            {
                valorPorcentaje = modificarTipoCalificacionAcademica.valorPorcentaje
            };

            return new TipoCalificacionAcademicaModelo
            {

                nombreTipoCalificacionAcademica = modificarTipoCalificacionAcademica.nombreTipoCalificacionAcademica,

                porcentajeModelo = porcentaje
            };

        }

        public async Task<ResultadoOperacion> ValidarInformacionRegistrarTipoCalificacionAcademicaAsync(RegistrarTipoCalificacionAcademica registrarTipoCalificacionAcademica)
        {
            TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo = MapearRegistrarTipoCalificacionAcademica(registrarTipoCalificacionAcademica);

            ResultadoOperacion resultadoOperacion = await tipoCalificacionAcademica.RegistrarTipoCalificacionAcademicaAsync(tipoCalificacionAcademicaModelo);

            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion> ValidarModificarInformacionTipoCalificacionAcademicaAsync(ModificarTipoCalificacionAcademica modificarTipoCalificacionAcademica)
        {
            TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo = MapearModificarTipoCalificacionAcademica(modificarTipoCalificacionAcademica);

            ResultadoOperacion resultadoOperacion = await tipoCalificacionAcademica.ModificarTipoCalificacionAcademicaAsync(tipoCalificacionAcademicaModelo);

            return resultadoOperacion;
        }

        public async Task<List<ListarTipoCalificacionAcademica>> ValidarInformacionTipoCalificacionAcademicaAsync()
        {
            var tipoCalificacionAcademicaModelo = await tipoCalificacionAcademica.InformacionTipoCalificacionAcademicaAsync();

            var resultado = MapearListarTipoCalificacionAcademica(tipoCalificacionAcademicaModelo);

            return resultado;
        }
    }
}

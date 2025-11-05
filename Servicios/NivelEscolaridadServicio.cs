using Colegio.Interfaz;
using Colegio.Modelos.Nivel_Escolaridad;
using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Salidas_Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Vistas;

namespace Colegio.Servicios
{
    public class NivelEscolaridadServicio
    {
        private readonly INivelEscolaridad nivelEscolaridad;

        public NivelEscolaridadServicio(INivelEscolaridad nivelEscolaridad)
        {
            this.nivelEscolaridad = nivelEscolaridad;
        }

        private NivelEscolaridadModelo MapearRegistrarNivelEscolaridad(RegistrarNivelEscolaridad registrarNivelEscolaridad)
        {
            return new NivelEscolaridadModelo
            {
                nombreNivelEscolaridad = registrarNivelEscolaridad.nombreNivelEscolaridad,
                pkIdNivelEscolaridad = 0
            };
        }

        private NivelEscolaridadModelo MapearGestionarEstadoNivelEscolaridad(GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad)
        {
            return new NivelEscolaridadModelo
            {
                nombreNivelEscolaridad = gestionarEstadoNivelEscolaridad.nombreNivelEscolaridad,
                pkIdNivelEscolaridad = 0,
                estadoNivelEscolaridad = string.Empty
            };
        }

        private List<ListarNivelEscolaridad> MapearListaNivelEscolaridad(List<NivelEscolaridadModelo> nivelEscolaridadModelo)
        {
            return nivelEscolaridadModelo.Select(modelo => new ListarNivelEscolaridad
            {
                nombreNivelEscolaridad = modelo.nombreNivelEscolaridad,
                estadoNivelEscolaridad = modelo.estadoNivelEscolaridad

            }).ToList();
        }

        private List<ListarNivelEscolaridadEstadoActivo> MapearListaNivelEscolaridadEstadoActivo(List<NivelEscolaridadModelo> nivelEscolaridadModelo)
        {
            return nivelEscolaridadModelo.Select(modelo => new ListarNivelEscolaridadEstadoActivo
            {
                nombreNivelEscolaridad = modelo.nombreNivelEscolaridad

            }).ToList();
        }

        public async Task<ResultadoMensajeNivelEscolaridad> ValidarInformacionRegistrarNivelEscolaridadAsync(RegistrarNivelEscolaridad registrarNivelEscolaridad)
        {
            NivelEscolaridadModelo nivelEscolaridadModelo = MapearRegistrarNivelEscolaridad(registrarNivelEscolaridad);

            ResultadoMensajeNivelEscolaridad resultado = await nivelEscolaridad.RegistrarNivelEscolaridadAsync(nivelEscolaridadModelo);

            return resultado;
        }
        public async Task<ResultadoMensajeNivelEscolaridad> ValidarGestionarEstadoNivelEscolaridadAsync(GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad)
        {
            NivelEscolaridadModelo nivelEscolaridadModelo = MapearGestionarEstadoNivelEscolaridad(gestionarEstadoNivelEscolaridad);

            ResultadoMensajeNivelEscolaridad resultado = await nivelEscolaridad.GestionarEstadoNivelEscolaridadAsync(gestionarEstadoNivelEscolaridad.nombreOperacion, nivelEscolaridadModelo);

            return resultado;
        }
        public async Task<List<ListarNivelEscolaridad>> ValidarInformacionNivelEscolaridadAsync()
        {
            var modeloNivelEscolaridad = await nivelEscolaridad.InformacionNivelEscolaridadAsync();

            var resultado = MapearListaNivelEscolaridad(modeloNivelEscolaridad);

            return resultado;
        }

        public async Task<List<ListarNivelEscolaridadEstadoActivo>> ValidarInformacionNivelEscolaridadEstadoActivoAsync()
        {
            var modeloNivelEscolaridad = await nivelEscolaridad.InformacionNivelEscolaridadEstadoActivoAsync();

            var resultado = MapearListaNivelEscolaridadEstadoActivo(modeloNivelEscolaridad);

            return resultado;
        }
    }
}

using Colegio.Interfaz;
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

        public async Task<ResultadoMensajeNivelEscolaridad> ValidarInformacionRegistrarNivelEscolaridadAsync(RegistrarNivelEscolaridad registrarNivelEscolaridad)
        {
            ResultadoMensajeNivelEscolaridad resultado = await nivelEscolaridad.RegistrarNivelEscolaridadAsync(registrarNivelEscolaridad);

            return resultado;
        }
        public async Task<ResultadoMensajeNivelEscolaridad> ValidarGestionarEstadoNivelEscolaridadAsync(GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad)
        {
            ResultadoMensajeNivelEscolaridad resultado = await nivelEscolaridad.GestionarEstadoNivelEscolaridadAsync(gestionarEstadoNivelEscolaridad);

            return resultado;
        }
        public async Task<List<ListarNivelEscolaridad>> ValidarInformacionNivelEscolaridadAsync()
        {
            var informacionNivelEscolaridad = await nivelEscolaridad.InformacionNivelEscolaridadAsync();
            return informacionNivelEscolaridad;
        }

        public async Task<List<ListarNivelEscolaridadEstadoActivo>> ValidarInformacionNivelEscolaridadEstadoActivoAsync()
        {
            var informacionNivelEscolaridad = await nivelEscolaridad.InformacionNivelEscolaridadEstadoActivoAsync();

            return informacionNivelEscolaridad;
        }
    }
}

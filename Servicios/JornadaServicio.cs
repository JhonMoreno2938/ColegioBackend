using Colegio.Interfaz;
using Colegio.Modelos.Jornada.Procedimientos;
using Colegio.Modelos.Jornada.Salidas_Procedimientos;
using Colegio.Modelos.Jornada.Vistas;

namespace Colegio.Servicios
{
    public class JornadaServicio
    {
        private readonly IJornada jornada;

        public JornadaServicio(IJornada jornada)
        {
            this.jornada = jornada;
        }

        public async Task<ResultadoMensajeJornada> ValidarInformacionRegistrarJornadaAsync(RegistrarJornada registrarJornada)
        {
            ResultadoMensajeJornada resultado = await jornada.RegistrarJornadaAsync(registrarJornada);

            return resultado;
        }
        public async Task<ResultadoMensajeJornada> ValidarGestionarEstadoJornadaAsync(GestionarEstadoJornada gestionarEstadoJornada)
        {
            ResultadoMensajeJornada resultado = await jornada.GestionarEstadoJornadaAsync(gestionarEstadoJornada);

            return resultado;
        }
        public async Task<List<ListarJornada>> ValidarInformacionJornadaAsync()
        {
            var informacionJornada = await jornada.InformacionJornadaAsync();
            return informacionJornada;
        }

        public async Task<List<ListarJornadaEstadoActivo>> ValidarInformacionJornadaEstadoActivoAsync()
        {
            var informacionJornada = await jornada.InformacionJornadaEstadoActivoAsync();

            return informacionJornada;
        }
    }
}

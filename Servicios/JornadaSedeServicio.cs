using Colegio.Interfaz;
using Colegio.Modelos.Jornada_Sede.Procedimientos;
using Colegio.Modelos.Jornada_Sede.Salidas_Procedimientos;
using Colegio.Modelos.Jornada_Sede;

namespace Colegio.Servicios
{
    public class JornadaSedeServicio
    {
        private readonly IJornadaSede jornadaSede;

        public JornadaSedeServicio(IJornadaSede jornadaSede)
        {
            this.jornadaSede = jornadaSede;
        }

        private string ConstruirListaJornadas(RegistrarJornadaSede registrarJornadaSede)
        {
            IEnumerable<string> nombresJornada = registrarJornadaSede.listaJornada.Select(j => j.nombreJornada);
            return string.Join(",", nombresJornada);
        }
       
        private List<SalidaMostrarJornadaAsociadaSede> MapearListaJornadaAsociadaSede(List<JornadaSedeModelo> jornadaSedeModelo)
        {
            return jornadaSedeModelo.Select(modelo => new SalidaMostrarJornadaAsociadaSede
            {
                nombreJornada = modelo.jornadaModelo.nombreJornada, 
                estadoJornadaSede = modelo.estadoJornadaSede
            }).ToList();
        }

        private List<SalidaMostrarJornadaAsociadaSedeEstadoActivo> MapearListaJornadaAsociadaSedeEstadoActivo(List<JornadaSedeModelo> jornadaSedeModelo)
        {
            return jornadaSedeModelo.Select(modelo => new SalidaMostrarJornadaAsociadaSedeEstadoActivo
            {
                nombreJornada = modelo.jornadaModelo.nombreJornada
            }).ToList();
        }

        public async Task<ResultadoMensajeJornadaSede> ValidarInformacionRegistrarJornadaSedeAsync(RegistrarJornadaSede registrarJornadaSede)
        {
            string listaJornada = ConstruirListaJornadas(registrarJornadaSede);

            ResultadoMensajeJornadaSede resultado = await jornadaSede.RegistrarJornadaSedeAsync(
                registrarJornadaSede.codigoDaneSede,
                listaJornada
            );

            return resultado;
        }

        public async Task<ResultadoMensajeJornadaSede> ValidarGestionarEstadoJornadaSedeAsync(GestionarEstadoJornadaSede gestionarEstadoJornadaSede)
        {

            ResultadoMensajeJornadaSede resultado = await jornadaSede.GestionarEstadoJornadaSedeAsync(
                gestionarEstadoJornadaSede.nombreOperacion,
                gestionarEstadoJornadaSede.codigoDane,
                gestionarEstadoJornadaSede.nombreJornada
                );

            return resultado;
        }

        public async Task<List<SalidaMostrarJornadaAsociadaSede>> ValidarMostrarJornadaAsociadaSedeAsync(string codigoDaneSede)
        {
            var modelosJornada = await jornadaSede.InformacionJornadaAsociadaSede(codigoDaneSede);

            List<SalidaMostrarJornadaAsociadaSede> resultado = MapearListaJornadaAsociadaSede(modelosJornada);

            return resultado;
        }

        public async Task<List<SalidaMostrarJornadaAsociadaSedeEstadoActivo>> ValidarMostrarJornadaAsociadaSedeEstadoActivoAsync(string codigoDaneSede)
        {
            var modelosJornada = await jornadaSede.InformacionJornadaAsociadaSedeEstadoActivo(codigoDaneSede);

            List<SalidaMostrarJornadaAsociadaSedeEstadoActivo> resultado = MapearListaJornadaAsociadaSedeEstadoActivo(modelosJornada);

            return resultado;
        }

    }
}
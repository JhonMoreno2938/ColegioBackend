using Colegio.Interfaz;
using Colegio.Modelos.Jornada;
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

        private JornadaModelo MapearRegistrarJornada(RegistrarJornada registrarJornada)
        {
            return new JornadaModelo
            {
                nombreJornada = registrarJornada.nombreJornada,
                pkIdJornada = 0
            };
        }

        private JornadaModelo MapearGestionarEstadoJornada(GestionarEstadoJornada gestionarEstadoJornada)
        {
            return new JornadaModelo
            {
                nombreJornada = gestionarEstadoJornada.nombreJornada,
                pkIdJornada = 0,
                estadoJornada = string.Empty
            };
        }

        private List<ListarJornada> MapearListarJornada(List<JornadaModelo> jornadaModelo)
        {
            return jornadaModelo.Select(modelo => new ListarJornada
            {
                nombreJornada = modelo.nombreJornada,
                estadoJornada = modelo.estadoJornada

            }).ToList();
        }

        private List<ListarJornadaEstadoActivo> MapearListarJornadaEstadoActivo(List<JornadaModelo> jornadaModelo)
        {
            return jornadaModelo.Select(modelo => new ListarJornadaEstadoActivo
            {
                nombreJornada = modelo.nombreJornada

            }).ToList();
        }

        public async Task<ResultadoMensajeJornada> ValidarInformacionRegistrarJornadaAsync(RegistrarJornada registrarJornada)
        {
            JornadaModelo jornadaModelo = MapearRegistrarJornada(registrarJornada);

            ResultadoMensajeJornada resultado = await jornada.RegistrarJornadaAsync(jornadaModelo);

            return resultado;
        }

        public async Task<ResultadoMensajeJornada> ValidarGestionarEstadoJornadaAsync(GestionarEstadoJornada gestionarEstadoJornada)
        {
            JornadaModelo jornadaModelo = MapearGestionarEstadoJornada(gestionarEstadoJornada);

            ResultadoMensajeJornada resultado = await jornada.GestionarEstadoJornadaAsync(gestionarEstadoJornada.nombreOperacion, jornadaModelo);

            return resultado;
        }

        public async Task<List<ListarJornada>> ValidarInformacionJornadaAsync()
        {
            var jornadaModelo = await jornada.InformacionJornadaAsync();

            var resultado = MapearListarJornada(jornadaModelo);

            return resultado;
        }
        
        public async Task<List<ListarJornadaEstadoActivo>> ValidarInformacionJornadaEstadoActivoAsync()
        {
            var jornadaModelo = await jornada.InformacionJornadaEstadoActivoAsync();

            var resultado = MapearListarJornadaEstadoActivo(jornadaModelo);
            
            return resultado;
        }


    }
}

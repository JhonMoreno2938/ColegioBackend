using Colegio.Modelos.Jornada;
using Colegio.Modelos.Jornada.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IJornada
    {
        Task<ResultadoMensajeJornada> RegistrarJornadaAsync(JornadaModelo jornadaModelo);
        Task<ResultadoMensajeJornada> GestionarEstadoJornadaAsync(string operacion, JornadaModelo jornadaModelo);
        Task<List<JornadaModelo>> InformacionJornadaAsync();
        Task<List<JornadaModelo>> InformacionJornadaEstadoActivoAsync();


    }
}

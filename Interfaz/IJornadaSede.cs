using Colegio.Modelos.Jornada_Sede;
using Colegio.Modelos.Jornada_Sede.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IJornadaSede
    {
        Task<ResultadoMensajeJornadaSede> RegistrarJornadaSedeAsync(string codigoDane, string listaJornada);
        Task<ResultadoMensajeJornadaSede> GestionarEstadoJornadaSedeAsync(string operacion, string codigoDane, string nombreJornada);
        Task<List<JornadaSedeModelo>> InformacionJornadaAsociadaSede(string codigoDaneSede);
        Task<List<JornadaSedeModelo>> InformacionJornadaAsociadaSedeEstadoActivo(string codigoDaneSede); 
    }
}

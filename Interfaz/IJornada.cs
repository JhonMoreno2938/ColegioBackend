using Colegio.Modelos.Jornada.Procedimientos;
using Colegio.Modelos.Jornada.Salidas_Procedimientos;
using Colegio.Modelos.Jornada.Vistas;

namespace Colegio.Interfaz
{
    public interface IJornada
    {
        Task<ResultadoMensajeJornada> RegistrarJornadaAsync(RegistrarJornada registrarJornada);
        Task<ResultadoMensajeJornada> GestionarEstadoJornadaAsync(GestionarEstadoJornada gestionarEstadoJornada);
        Task<List<ListarJornada>> InformacionJornadaAsync();
        Task<List<ListarJornadaEstadoActivo>> InformacionJornadaEstadoActivoAsync();


    }
}

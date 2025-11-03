using Colegio.Modelos.Sede.Procedimientos;
using Colegio.Modelos.Sede.Salidas_Procedimientos;
using Colegio.Modelos.Sede.Vistas;

namespace Colegio.Interfaz
{
    public interface ISede
    {
        Task<ResultadoMensajeSede> RegistrarSedeAsync(RegistrarSede registrarSede);
        Task<ResultadoMensajeSede> ModificarInformacionSedeAsync(ModificarInformacionSede modificarInformacionSede);
        Task<ResultadoMensajeSede> GestionarEstadoSedeAsync(GestionarEstadoSede gestionarEstadoSede);
        Task<SalidaConsultarSede> ConsultarSedeAsync(string codigoDaneSede);
        Task<List<ListarSede>> InformacionSedeAsync();
        Task<List<ListarSedeEstadoActivo>> InformacionSedeEstadoActivoAsync();

    }
}

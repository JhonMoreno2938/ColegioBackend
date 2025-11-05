using Colegio.Modelos.Sede;
using Colegio.Modelos.Sede.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface ISede
    {
        Task<ResultadoMensajeSede> RegistrarSedeAsync(SedeModelo sedeModelo);
        Task<ResultadoMensajeSede> ModificarInformacionSedeAsync(SedeModelo sedeModelo);
        Task<ResultadoMensajeSede> GestionarEstadoSedeAsync(string operacion, SedeModelo sedeModelo);
        Task<SalidaConsultarSede> ConsultarSedeAsync(string codigoDaneSede);
        Task<List<SedeModelo>> InformacionSedeAsync();
        Task<List<SedeModelo>> InformacionSedeEstadoActivoAsync();

    }
}

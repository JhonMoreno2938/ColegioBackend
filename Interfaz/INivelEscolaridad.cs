using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Salidas_Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Vistas;

namespace Colegio.Interfaz
{
    public interface INivelEscolaridad
    {
        Task<ResultadoMensajeNivelEscolaridad> RegistrarNivelEscolaridadAsync(RegistrarNivelEscolaridad registrarNivelEscolaridad);
        Task<ResultadoMensajeNivelEscolaridad> GestionarEstadoNivelEscolaridadAsync(GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad);
        Task<List<ListarNivelEscolaridad>> InformacionNivelEscolaridadAsync();
        Task<List<ListarNivelEscolaridadEstadoActivo>> InformacionNivelEscolaridadEstadoActivoAsync();
    }
}

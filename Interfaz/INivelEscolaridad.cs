using Colegio.Modelos.Nivel_Escolaridad;
using Colegio.Modelos.Nivel_Escolaridad.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface INivelEscolaridad
    {
        Task<ResultadoMensajeNivelEscolaridad> RegistrarNivelEscolaridadAsync(NivelEscolaridadModelo nivelEscolaridadModelo);
        Task<ResultadoMensajeNivelEscolaridad> GestionarEstadoNivelEscolaridadAsync(string opreacion, NivelEscolaridadModelo nivelEscolaridadModelo);
        Task<List<NivelEscolaridadModelo>> InformacionNivelEscolaridadAsync();
        Task<List<NivelEscolaridadModelo>> InformacionNivelEscolaridadEstadoActivoAsync();
    }
}

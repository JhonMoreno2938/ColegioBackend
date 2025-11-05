using Colegio.Modelos.Grado;
using Colegio.Modelos.Grado.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IGrado
    {
        Task<ResultadoMensajeGrado> RegistrarGradoAsync(GradoModelo gradoModelo);
        Task<ResultadoMensajeGrado> GestionarEstadoGradoAsync(string operacion, GradoModelo gradoModelo);
        Task<List<GradoModelo>> InformacionGradoAsync();
        Task<List<GradoModelo>> InformacionGradoEstadoActivoAsync();
    }
}

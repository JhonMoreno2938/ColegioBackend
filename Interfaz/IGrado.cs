using Colegio.Modelos.Grado.Procedimientos;
using Colegio.Modelos.Grado.Salidas_Procedimientos;
using Colegio.Modelos.Grado.Vistas;

namespace Colegio.Interfaz
{
    public interface IGrado
    {
        Task<ResultadoMensajeGrado> RegistrarGradoAsync(RegistrarGrado registrarGrado);
        Task<ResultadoMensajeGrado> GestionarEstadoGradoAsync(GestionarEstadoGrado gestionarEstadoGrado);
        Task<List<ListarGrado>> InformacionGradoAsync();
        Task<List<ListarGradoEstadoActivo>> InformacionGradoEstadoActivoAsync();
    }
}

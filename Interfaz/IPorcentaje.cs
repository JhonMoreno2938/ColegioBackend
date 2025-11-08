using Colegio.Modelos.Porcentaje;
using Colegio.Modelos.Porcentaje.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IPorcentaje
    {
        Task<ResultadoMensajePorcentaje> RegistrarPorcentajeAsync(PorcentajeModelo porcentajeModelo);
        Task<ResultadoMensajePorcentaje> GestionarEstadoPorcentajeAsync(string operacion, PorcentajeModelo porcentajeModelo);
        Task<List<PorcentajeModelo>> InformacionPorcentajeAsync();
        Task<List<PorcentajeModelo>> InformacionPorcentajeEstadoActivoAsync();


    }
}

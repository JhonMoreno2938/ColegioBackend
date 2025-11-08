using Colegio.Interfaz;
using Colegio.Modelos.Porcentaje;
using Colegio.Modelos.Porcentaje.Procedimientos;
using Colegio.Modelos.Porcentaje.Salidas_Procedimientos;
using Colegio.Modelos.Porcentaje.Vistas;

namespace Colegio.Servicios
{
    public class PorcentajeServicio
    {
        private readonly IPorcentaje porcentaje;

        public PorcentajeServicio(IPorcentaje porcentaje)
        {
            this.porcentaje = porcentaje;
        }

        private PorcentajeModelo MapearRegistrarPorcentaje(RegistrarPorcentaje registrarPorcentaje)
        {
            return new PorcentajeModelo
            {
                valorPorcentaje = registrarPorcentaje.valorPorcentaje,
                pkIdPorcentaje = 0
            };

        }

        private PorcentajeModelo MapearGestionarEstadoPorcentaje(GestionarEstadoPorcentaje gestionarEstadoPorcentaje)
        {
            return new PorcentajeModelo
            {
                valorPorcentaje = gestionarEstadoPorcentaje.valorPorcentaje,
                pkIdPorcentaje = 0,
                estadoPorcentaje = string.Empty
                
            };
        }

        private List<ListarPorcentaje> MapearListarPorcentaje(List<PorcentajeModelo> porcentajeModelo)
        {
            return porcentajeModelo.Select(modelo => new ListarPorcentaje
            {
                valorPorcentaje = modelo.valorPorcentaje,
                estadoPorcentaje = modelo.estadoPorcentaje

            }).ToList();
        } 

        private List<ListarPorcentajeEstadoActivo> MapearListarPorcentajeEstadoActivo(List<PorcentajeModelo> porcentajeModelo)
        {
            return porcentajeModelo.Select(modelo => new ListarPorcentajeEstadoActivo
            {
                valorPorcentaje = modelo.valorPorcentaje

            }).ToList();
        }


        public async Task<ResultadoMensajePorcentaje> ValidarInformacionRegistrarPorcentajeAsync(RegistrarPorcentaje registrarPorcentaje)
        {
            PorcentajeModelo porcentajeModelo = MapearRegistrarPorcentaje(registrarPorcentaje);

            ResultadoMensajePorcentaje resultado = await porcentaje.RegistrarPorcentajeAsync(porcentajeModelo);

            return resultado;
        }

        public async Task<ResultadoMensajePorcentaje> ValidarInformacionGestionarEstadoPorcentajeAsync(GestionarEstadoPorcentaje gestionarEstadoPorcentaje)
        {
            PorcentajeModelo porcentajeModelo = MapearGestionarEstadoPorcentaje(gestionarEstadoPorcentaje);

            ResultadoMensajePorcentaje resultado = await porcentaje.GestionarEstadoPorcentajeAsync(gestionarEstadoPorcentaje.nombreOperacion, porcentajeModelo);

            return resultado;
        }

        public async Task<List<ListarPorcentaje>> ValidarInformacionPorcentajeAsync()
        {
            var modeloPorcentaje = await porcentaje.InformacionPorcentajeAsync();

            var resultado = MapearListarPorcentaje(modeloPorcentaje);

            return resultado;
        }

        public async Task<List<ListarPorcentajeEstadoActivo>> ValidarInformacionPorcentajeEstadoAsync()
        {
            var modeloPorcentaje = await porcentaje.InformacionPorcentajeEstadoActivoAsync();

            var resultado = MapearListarPorcentajeEstadoActivo(modeloPorcentaje);

            return resultado;
        }
    }
}

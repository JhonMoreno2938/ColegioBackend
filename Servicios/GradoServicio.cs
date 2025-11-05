using Colegio.Interfaz;
using Colegio.Modelos.Grado;
using Colegio.Modelos.Grado.Procedimientos;
using Colegio.Modelos.Grado.Salidas_Procedimientos;
using Colegio.Modelos.Grado.Vistas;

namespace Colegio.Servicios
{
    public class GradoServicio
    {
        private readonly IGrado grado;

        public GradoServicio(IGrado grado)
        {
            this.grado = grado;
        }

        private GradoModelo MapearRegistrarGrado(RegistrarGrado registrarGrado)
        {
            return new GradoModelo
            {
                nombreGrado = registrarGrado.nombreGrado,
                pkIdGrado = 0
            };

        }

        private GradoModelo MapearGestionarEstadoGrado(GestionarEstadoGrado gestionarEstadoGrado)
        {
            return new GradoModelo
            {
                nombreGrado = gestionarEstadoGrado.nombreGrado,
                pkIdGrado = 0,
                estadoGrado = string.Empty
            };
        }

        private List<ListarGrado> MapearListarGrado(List<GradoModelo> gradoModelo)
        {
            return gradoModelo.Select(modelo => new ListarGrado
            {
                nombreGrado = modelo.nombreGrado,
                estadoGrado = modelo.estadoGrado
            }).ToList();
        }

        private List<ListarGradoEstadoActivo> MapearListarGradoEstadoActivo(List<GradoModelo> gradoModelo)
        {
            return gradoModelo.Select(modelo => new ListarGradoEstadoActivo
            {
                nombreGrado = modelo.nombreGrado
            }).ToList();
        }

        public async Task<ResultadoMensajeGrado> ValidarInformacionRegistrarGradoAsync(RegistrarGrado registrarGrado)
        {
            GradoModelo gradoModelo = MapearRegistrarGrado(registrarGrado);

            ResultadoMensajeGrado resultado = await grado.RegistrarGradoAsync(gradoModelo);

            return resultado;
        }

        public async Task<ResultadoMensajeGrado> ValidarGestionarEstadoGradoAsync(GestionarEstadoGrado gestionarEstadoGrado)
        {
            GradoModelo gradoModelo = MapearGestionarEstadoGrado(gestionarEstadoGrado);

            ResultadoMensajeGrado resultado = await grado.GestionarEstadoGradoAsync(gestionarEstadoGrado.nombreOperacion, gradoModelo);

            return resultado;
        }

        public async Task<List<ListarGrado>> ValidarInformacionGradoAsync()
        {
            var modeloGrado = await grado.InformacionGradoAsync();

            var resultado = MapearListarGrado(modeloGrado);

            return resultado;
        }

        public async Task<List<ListarGradoEstadoActivo>> ValidarInformacionGradoEstadoActivoAsync()
        {
            var modeloGrado = await grado.InformacionGradoEstadoActivoAsync();

            var resultado = MapearListarGradoEstadoActivo(modeloGrado);

            return resultado;
        }
    }
}

using Colegio.Interfaz;
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

        public async Task<ResultadoMensajeGrado> ValidarInformacionRegistrarGradoAsync(RegistrarGrado registrarGrado)
        {
            ResultadoMensajeGrado resultado = await grado.RegistrarGradoAsync(registrarGrado);

            return resultado;
        }
        public async Task<ResultadoMensajeGrado> ValidarGestionarEstadoGradoAsync(GestionarEstadoGrado gestionarEstadoGrado)
        {
            ResultadoMensajeGrado resultado = await grado.GestionarEstadoGradoAsync(gestionarEstadoGrado);

            return resultado;
        }

        public async Task<List<ListarGrado>> ValidarInformacionGradoAsync()
        {
            var informacionGrado = await grado.InformacionGradoAsync();

            return informacionGrado;
        }

        public async Task<List<ListarGradoEstadoActivo>> ValidarInformacionGradoEstadoActivoAsync()
        {
            var informacionGrado = await grado.InformacionGradoEstadoActivoAsync();

            return informacionGrado;
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Sede.Procedimientos;
using Colegio.Modelos.Sede.Salidas_Procedimientos;
using Colegio.Modelos.Sede.Vistas;

namespace Colegio.Servicios
{
    public class SedeServicio
    {
        private readonly ISede sede;

        public SedeServicio(ISede sede)
        {
            this.sede = sede;
        }

        public async Task<ResultadoMensajeSede> ValidarInformacionRegistrarSedeAsync(RegistrarSede registrarSede)
        {
            ResultadoMensajeSede resultado = await sede.RegistrarSedeAsync(registrarSede);

            return resultado;
        }

        public async Task<ResultadoMensajeSede> ValidarModificarInformacionSedeAsync(ModificarInformacionSede modificarInformacionSede)
        {
            ResultadoMensajeSede resultado = await sede.ModificarInformacionSedeAsync(modificarInformacionSede);

            return resultado;
        }

        public async Task<ResultadoMensajeSede> ValidarGestionarEstadoSedeAsync(GestionarEstadoSede gestionarEstadoSede)
        {
            ResultadoMensajeSede resultado = await sede.GestionarEstadoSedeAsync(gestionarEstadoSede);

            return resultado;
        }

        public async Task<SalidaConsultarSede> ValidarConsultarSedeAsync(string codigoDaneSede)
        {
            SalidaConsultarSede resultado = await sede.ConsultarSedeAsync(codigoDaneSede);

            return resultado;
        }

        public async Task<List<ListarSede>> ValidarInformacionSedeAsync()
        {
            var informacionSede = await sede.InformacionSedeAsync();

            return informacionSede;
        }
        public async Task<List<ListarSedeEstadoActivo>> ValidarInformacionSedeEstadoActivoAsync()
        {
            var informacionSedeEstadoActivo = await sede.InformacionSedeEstadoActivoAsync();

            return informacionSedeEstadoActivo;
        }


    }
}

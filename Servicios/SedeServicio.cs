using Colegio.Interfaz;
using Colegio.Modelos.Grado.Vistas;
using Colegio.Modelos.Grado;
using Colegio.Modelos.Sede;
using Colegio.Modelos.Sede.Procedimientos;
using Colegio.Modelos.Sede.Salidas_Procedimientos;
using Colegio.Modelos.Sede.Vistas;
using Colegio.Modelos.Tipo_Sede;
using Colegio.Modelos.Grado.Procedimientos;

namespace Colegio.Servicios
{
    public class SedeServicio
    {
        private readonly ISede sede;

        public SedeServicio(ISede sede)
        {
            this.sede = sede;
        }

        private SedeModelo MapearRegistrarSede(RegistrarSede registrarSede)
        {
            var tipoSede = new TipoSedeModelo()
            {
                pkIdTipoSede = 0,
                nombreTipoSede = registrarSede.nombreTipoSede
            };

            return new SedeModelo
            {
                tipoSedeModelo = tipoSede,

                codigoDaneSede = registrarSede.codigoDaneSede,
                nombreSede = registrarSede.nombreSede,
                direccionSede = registrarSede.direccionSede,
                numeroContactoSede = registrarSede.numeroContactoSede,
            };
        }

        private SedeModelo MapearModificarInformacionSede(ModificarInformacionSede modificarInformacionSede)
        {
            return new SedeModelo
            {

                codigoDaneSede = modificarInformacionSede.codigoDaneSede,
                nombreSede = modificarInformacionSede.nombreSede,
                direccionSede = modificarInformacionSede.direccionSede,
                numeroContactoSede = modificarInformacionSede.numeroContactoSede,
            };

        }

        private SedeModelo MapearGestionarEstadoSede(GestionarEstadoSede gestionarEstadoSede)
        {
            return new SedeModelo
            {
                codigoDaneSede = gestionarEstadoSede.codigoDaneSede,
                pkIdSede = 0,
                estadoSede = string.Empty
            };
        }

        private List<ListarSede> MapearListarSede(List<SedeModelo> sedeModelo)
        {
            return sedeModelo.Select(modelo => new ListarSede
            {
                codigoDaneSede = modelo.codigoDaneSede,
                nombreSede = modelo.nombreSede,
                estadoSede = modelo.estadoSede
            }).ToList();
        }

        private List<ListarSedeEstadoActivo> MapearListarSedeEstadoActivo(List<SedeModelo> sedeModelo)
        {
            return sedeModelo.Select(modelo => new ListarSedeEstadoActivo
            {
                codigoDaneSede = modelo.codigoDaneSede,
                nombreSede = modelo.nombreSede
            }).ToList();
        }

        public async Task<ResultadoMensajeSede> ValidarInformacionRegistrarSedeAsync(RegistrarSede registrarSede)
        {
            SedeModelo sedeModelo = MapearRegistrarSede(registrarSede);

            ResultadoMensajeSede resultado = await sede.RegistrarSedeAsync(sedeModelo);

            return resultado;
        }

        public async Task<ResultadoMensajeSede> ValidarModificarInformacionSedeAsync(ModificarInformacionSede modificarInformacionSede)
        {
            SedeModelo sedeModelo = MapearModificarInformacionSede(modificarInformacionSede);

            ResultadoMensajeSede resultado = await sede.ModificarInformacionSedeAsync(sedeModelo);

            return resultado;
        }

        public async Task<ResultadoMensajeSede> ValidarGestionarEstadoSedeAsync(GestionarEstadoSede gestionarEstadoSede)
        {
            SedeModelo sedeModelo = MapearGestionarEstadoSede(gestionarEstadoSede);

            ResultadoMensajeSede resultado = await sede.GestionarEstadoSedeAsync(gestionarEstadoSede.nombreOperacion, sedeModelo);

            return resultado;
        }

        public async Task<SalidaConsultarSede> ValidarConsultarSedeAsync(string codigoDaneSede)
        {
            SalidaConsultarSede resultado = await sede.ConsultarSedeAsync(codigoDaneSede);

            return resultado;
        }

        public async Task<List<ListarSede>> ValidarInformacionSedeAsync()
        {
            var sedeModelo = await sede.InformacionSedeAsync();

            var resultado = MapearListarSede(sedeModelo);

            return resultado;
        }
        public async Task<List<ListarSedeEstadoActivo>> ValidarInformacionSedeEstadoActivoAsync()
        {
            var sedeModelo = await sede.InformacionSedeEstadoActivoAsync();

            var resultado = MapearListarSedeEstadoActivo(sedeModelo);

            return resultado;
        }


    }
}

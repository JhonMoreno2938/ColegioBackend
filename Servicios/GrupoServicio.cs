using Colegio.Interfaz;
using Colegio.Modelos.Grupo.Procedimientos;
using Colegio.Modelos.Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grupo.Vistas;

namespace Colegio.Servicios
{
    public class GrupoServicio
    {
        private readonly IGrupo grupo;

        public GrupoServicio(IGrupo grupo)
        {
            this.grupo = grupo;
        }

        public async Task<ResultadoMensajeGrupo> ValidarInformacionRegistrarGrupoAsync(RegistrarGrupo registrarGrupo)
        {
            ResultadoMensajeGrupo resultado = await grupo.RegistrarGrupoAsync(registrarGrupo);

            return resultado;
        }
        public async Task<ResultadoMensajeGrupo> ValidarGestionarEstadoGrupoAsync(GestionarEstadoGrupo gestionarEstadoGrupo)
        {
            ResultadoMensajeGrupo resultado = await grupo.GestionarEstadoGrupoAsync(gestionarEstadoGrupo);

            return resultado;
        }
        public async Task<List<ListarGrupo>> ValidarInformacionGrupoAsync()
        {
            var informacionGrupo = await grupo.InformacionGrupoAsync();

            return informacionGrupo;
        }

        public async Task<List<ListarGrupoEstadoActivo>> ValidarInformacionGrupoEstadoActivoAsync()
        {
            var informacionGrupo = await grupo.InformacionGrupoEstadoActivoAsync();

            return informacionGrupo;
        }
    }
}

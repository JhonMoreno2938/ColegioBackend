using Colegio.Interfaz;
using Colegio.Modelos.Grupo;
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

        private GrupoModelo MapearRegistrarGrupo(RegistrarGrupo registrarGrupo)
        {
            return new GrupoModelo
            {
                nombreGrupo = registrarGrupo.nombreGrupo,
                pkIdGrupo = 0
            };
        }

        private GrupoModelo MapearGestionarEstadoGrupo(GestionarEstadoGrupo gestionarEstadoGrupo)
        {
            return new GrupoModelo
            {
                nombreGrupo = gestionarEstadoGrupo.nombreGrupo,
                pkIdGrupo = 0,
                estadoGrupo = string.Empty
            };

        }

        private List<ListarGrupo> MapearListarGrupo(List<GrupoModelo> grupoModelo)
        {
            return grupoModelo.Select(modelo => new ListarGrupo
            {
                nombreGrupo = modelo.nombreGrupo,
                estadoGrupo = modelo.estadoGrupo
            }).ToList();
        }

        private List<ListarGrupoEstadoActivo> MapearListarGrupoEstadoActivo(List<GrupoModelo> grupoModelo)
        {
            return grupoModelo.Select(modelo => new ListarGrupoEstadoActivo
            {
                nombreGrupo = modelo.nombreGrupo
            }).ToList();
        }

        public async Task<ResultadoMensajeGrupo> ValidarInformacionRegistrarGrupoAsync(RegistrarGrupo registrarGrupo)
        {
            GrupoModelo grupoModelo = MapearRegistrarGrupo(registrarGrupo);

            ResultadoMensajeGrupo resultado = await grupo.RegistrarGrupoAsync(grupoModelo);

            return resultado;
        }
        public async Task<ResultadoMensajeGrupo> ValidarGestionarEstadoGrupoAsync(GestionarEstadoGrupo gestionarEstadoGrupo)
        {
            GrupoModelo grupoModelo = MapearGestionarEstadoGrupo(gestionarEstadoGrupo);

            ResultadoMensajeGrupo resultado = await grupo.GestionarEstadoGrupoAsync(gestionarEstadoGrupo.nombreOperacion, grupoModelo);

            return resultado;
        }
        public async Task<List<ListarGrupo>> ValidarInformacionGrupoAsync()
        {
            var modeloGrupo = await grupo.InformacionGrupoAsync();

            var resultado = MapearListarGrupo(modeloGrupo);

            return resultado;
        }

        public async Task<List<ListarGrupoEstadoActivo>> ValidarInformacionGrupoEstadoActivoAsync()
        {
            var modeloGrupo = await grupo.InformacionGrupoEstadoActivoAsync();

            var resultado = MapearListarGrupoEstadoActivo(modeloGrupo);

            return resultado;
        }
    }
}

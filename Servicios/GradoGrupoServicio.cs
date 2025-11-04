using Colegio.Interfaz;
using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Vistas;

namespace Colegio.Servicios
{
    public class GradoGrupoServicio
    {
        private readonly IGradoGrupo gradoGrupo;

        public GradoGrupoServicio(IGradoGrupo gradoGrupo)
        {
            this.gradoGrupo = gradoGrupo;
        }

        public async Task<ResultadoMensajeGradoGrupo> ValidarInformacionRegistrarGradoGrupoNivelEscolaridadAsync(RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad)
        {
            ResultadoMensajeGradoGrupo resultado = await gradoGrupo.RegistrarGradoGrupoNivelEscolaridadAsync(registrarGradoGrupoNivelEscolaridad);

            return resultado;
        }
        public async Task<ResultadoMensajeGradoGrupo> ValidarGestionarGradoGrupoNivelEscolaridadAsync(GestionarEstadoGradoGrupoNivelEscolaridad gestionarEstadoGradoGrupoNivelEscolaridad)
        {
            ResultadoMensajeGradoGrupo resultado = await gradoGrupo.GestionarEstadoGradoGrupoNivelEscolaridadAsync(gestionarEstadoGradoGrupoNivelEscolaridad);

            return resultado;
        }
        public async Task<List<ListarGradoGrupo>> ValidarInformacionGradoGrupoAsync()
        {
            var informacionGradoGrupo = await gradoGrupo.InformacionGradoGrupoAsync();
            return informacionGradoGrupo;
        }

        public async Task<List<ListarGradoGrupoEstadoActivo>> ValidarInformacionGradoGrupoEstadoActivoAsync()
        {
            var informacionGradoGrupo = await gradoGrupo.InformacionGradoGrupoEstadoActivoAsync();

            return informacionGradoGrupo;
        }
    }
}

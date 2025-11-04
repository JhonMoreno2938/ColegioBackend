using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Vistas;

namespace Colegio.Interfaz
{
    public interface IGradoGrupo
    {
        Task<ResultadoMensajeGradoGrupo> RegistrarGradoGrupoNivelEscolaridadAsync(RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad);
        Task<ResultadoMensajeGradoGrupo> GestionarEstadoGradoGrupoNivelEscolaridadAsync(GestionarEstadoGradoGrupoNivelEscolaridad gestionarEstadoGradoGrupoNivelEscolaridad);
        Task<List<ListarGradoGrupo>> InformacionGradoGrupoAsync();
        Task<List<ListarGradoGrupoEstadoActivo>> InformacionGradoGrupoEstadoActivoAsync();
    }
}

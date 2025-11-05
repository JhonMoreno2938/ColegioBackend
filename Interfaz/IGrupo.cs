using Colegio.Modelos.Grupo;
using Colegio.Modelos.Grupo.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IGrupo
    {
        Task<ResultadoMensajeGrupo> RegistrarGrupoAsync(GrupoModelo grupoModelo);
        Task<ResultadoMensajeGrupo> GestionarEstadoGrupoAsync(string operacion, GrupoModelo grupoModelo);
        Task<List<GrupoModelo>> InformacionGrupoAsync();
        Task<List<GrupoModelo>> InformacionGrupoEstadoActivoAsync();
    }
}

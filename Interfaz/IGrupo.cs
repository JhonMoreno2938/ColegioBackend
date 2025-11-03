using Colegio.Modelos.Grupo.Procedimientos;
using Colegio.Modelos.Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grupo.Vistas;

namespace Colegio.Interfaz
{
    public interface IGrupo
    {
        Task<ResultadoMensajeGrupo> RegistrarGrupoAsync(RegistrarGrupo registrarGrupo);
        Task<ResultadoMensajeGrupo> GestionarEstadoGrupoAsync(GestionarEstadoGrupo gestionarEstadoGrupo);
        Task<List<ListarGrupo>> InformacionGrupoAsync();
        Task<List<ListarGrupoEstadoActivo>> InformacionGrupoEstadoActivoAsync();
    }
}

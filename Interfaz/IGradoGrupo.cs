using Colegio.Modelos.Grado_Grupo;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Vistas;

namespace Colegio.Interfaz
{
    public interface IGradoGrupo
    {
       Task<ResultadoMensajeGradoGrupo> RegistrarGradoGrupoNivelEscolaridadAsync(GradoGrupoModelo gradoGrupoModelo);
        Task<ResultadoMensajeGradoGrupo> GestionarEstadoGradoGrupoNivelEscolaridadAsync(string operacion, string nombreGrado, string nombreGrupo, string nombreNivelEscolaridad);
        Task<List<ListaGradoGrupoModelo>> InformacionGradoGrupoAsync();
        Task<List<ListaGradoGrupoModelo>> InformacionGradoGrupoEstadoActivoAsync();
    }
}

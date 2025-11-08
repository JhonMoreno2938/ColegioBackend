using Colegio.Modelos.Asignatura_Grado_Grupo.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IAsignaturaGradoGrupo
    {
        Task<ResultadoMensajeAsignaturaGradoGrupo> RegistrarAsignaturaGradoGrupoAsync(string nombreAsignatura, string listaGrado, string listaGrupo, string listaNivelEscolaridad);
        Task<ResultadoMensajeAsignaturaGradoGrupo> GestionarEstadoAsignaturaGradoGrupoAsync(string operacion, string nombreGrado, string nombreGrupo, string nombreNivelEscolaridad, string nombreAsignatura);
    }
}

using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface IFuncionarioAsignatura
    {
        Task<bool> AsignarFuncionarioAsignaturaAsync(string numeroDocumento, string nombreAsignatura, string listaGrado, string listaGrupo, string listaSede, string listaJornada);
        Task<ResultadoOperacion> GestionarEstadoFuncionarioAsignaturaAsync(string operacion, int idFuncionarioAsignaturaGradoGrupo);

    }
}

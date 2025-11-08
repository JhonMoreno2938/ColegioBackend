using Colegio.Modelos.Asignatura;
using Colegio.Modelos.Asignatura.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IAsignatura
    {
        Task<SalidaConsultarAsignatura> ConsultarAsignaturaAsync(string nombreAsignatura);
        Task<List<SalidaConsultarGradoGrupoAsignaturaEstadoActivo>> ConsultarGradoGrupoAsignaturaEstadoActivoAsync(string nombreAsignatura);
        Task<List<AsignaturaModelo>> InformacionAsignaturaAsync();
        Task<List<AsignaturaModelo>> InformacionAsignaturaEstadoActivoAsync();
    }
}

using Colegio.Modelos.Estudiante;
using Colegio.Modelos.Estudiante.Consultas;
using Colegio.Modelos.Estudiante.Vistas;
using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface IEstudiante
    {
        Task<ResultadoConID> RegistrarEstudianteAsync(EstudianteModelo estudianteModelo, string sede, string jornada, string grado, string grupo);
        Task<ResultadoOperacion> CargueEstudianteCSVAsync(EstudianteModelo estudianteModelo, string nombreTipoDocumento, string sede, string jornada, string grado, string grupo, int annioActual);
        Task<ResultadoOperacion> ModificarInformacionEstudianteAsync(EstudianteModelo estudianteModelo);
        Task<ConsultarEstudiante> ConsultarEstudianteAsync(string numeroDocumento);
        Task<List<ListarEstudiante>> InformacionEstudianteAsync();
        Task<ResultadoOperacion> ProcesarCargueEstudianteAsync();
    }
}

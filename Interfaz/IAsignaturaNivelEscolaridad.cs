using Colegio.Modelos.Asignatura_Nivel_Escolaridad.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IAsignaturaNivelEscolaridad
    {
        Task<ResultadoMensajeAsignatuarNivelEscolaridad> RegistrarAsignaturaAsync(string nombreAsignatura, string listaIntensidadHoraria, string listaNivelEscolaridad);
    }
}

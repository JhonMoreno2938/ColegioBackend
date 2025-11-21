using Colegio.Modelos.Tipo_Calificacion_Academica;
using Colegio.Utilidades;

namespace Colegio.Interfaz
{
    public interface ITipoCalificacionAcademica
    {
        Task<ResultadoOperacion> RegistrarTipoCalificacionAcademicaAsync(TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo);
        Task<ResultadoOperacion> ModificarTipoCalificacionAcademicaAsync(TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo);
        Task<List<TipoCalificacionAcademicaModelo>> InformacionTipoCalificacionAcademicaAsync();
    }
}

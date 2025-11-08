using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nombre_Periodo_Academico.Vistas
{
    public class ListarNombrePeriodoAcademico
    {
        [JsonPropertyName("nombrePeriodoAcademico")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;
    }
}

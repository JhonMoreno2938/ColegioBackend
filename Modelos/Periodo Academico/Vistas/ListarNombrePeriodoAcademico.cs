using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Vistas
{
    public class ListarNombrePeriodoAcademico
    {
        [JsonPropertyName("idPeriodoAcademico")]
        public int idPeriodoAcademico { get; set; } = 0;

        [JsonPropertyName("nombrePeriodoAcademici")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Salidas_Procedimientos
{
    public class SalidaObtenerPeriodoAcademicoAnnio
    {
        [JsonPropertyName("idPeriodoAcademico")]
        public int idPeriodoAcademico { get; set; } = 0;

        [JsonPropertyName("fechaInicioPeriodoAcademico")]
        public string fechaInicioPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalPeriodoAcademico")]
        public string fechaFinalPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("estadoPeriodoAcademico")]
        public string estadoPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("nombrePeriodoAcademico")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        public string valorPorcentaje { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Salidas_Procedimientos
{
    public class SalidaConsultarPeriodoAcademico
    {
        public bool exito { get; set; }

        public string mensaje { get; set; }

        [JsonPropertyName("pkIdPeriodoAcademico")]
        public int pkIdPeriodoAcademico { get; set; } = 0;

        [JsonPropertyName("fechaInicioPeriodoAcademico")]
        public string fechaInicioPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalPeriodoAcademico")]
        public string fechaFinalPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("estadoPeriodoAcademico")]
        public string estadoPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("nombrePeriodoAcademico")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;
    }
}

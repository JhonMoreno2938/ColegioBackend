using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Procedimientos
{
    public class RegistrarPeriodoAcademico
    {
        [JsonPropertyName("nombrePeriodoAcademico")]
        [StringLength(20)]
        [Required]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        [Required]
        public int valorPorcentaje { get; set; } = 0;

        [JsonPropertyName("fechaInicioPeriodoAcademico")]
        [StringLength(10)]
        [Required]
        public string fechaInicioPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalPeriodoAcademico")]
        [StringLength(10)]
        [Required]
        public string fechaFinalPeriodoAcademico { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Procedimientos
{
    public class ModificarPeriodoAcademico
    {
        [JsonPropertyName("idPeriodoAcademico")]
        [Required]
        public int idPeriodoAcademico { get; set; } = 0;

        [JsonPropertyName("fechaInicioPeriodoAcademico")]
        [StringLength(10)]
        public string fechaInicioPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalPeriodoAcademico")]
        [StringLength(10)]
        public string fechaFinalPeriodoAcademico { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;
    }
}

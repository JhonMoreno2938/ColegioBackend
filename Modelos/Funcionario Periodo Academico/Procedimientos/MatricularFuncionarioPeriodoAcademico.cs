using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario_Periodo_Academico.Procedimientos
{
    public class MatricularFuncionarioPeriodoAcademico
    {
        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("fechaInicioHabilitacion")]
        [StringLength(10)]
        [Required]
        public string fechaInicioHabilitacion { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalHabilitacion")]
        [StringLength(10)]
        [Required]
        public string fechaFinalHabilitacion { get; set; } = string.Empty;

        [JsonPropertyName("idPeriodoAcademico")]
        [Required]
        public int idPeriodoAcademico { get; set; } = 0;
    }
}

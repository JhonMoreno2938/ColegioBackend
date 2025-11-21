using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario_Periodo_Academico.Procedimientos
{
    public class MatricularFuncionariosPeriodoAcademico
    {
        [JsonPropertyName("fechaInicioHabilitiacion")]
        [StringLength(10)]
        [Required]
        public string fechaInicioHabilitacion { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalHabilitacion")]
        [StringLength(10)]
        [Required]
        public string fechaFinalHabilitacion { get; set; } = string.Empty;
    }
}

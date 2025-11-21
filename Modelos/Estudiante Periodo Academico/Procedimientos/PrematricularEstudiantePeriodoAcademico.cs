using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante_Periodo_Academico.Procedimientos
{
    public class PrematricularEstudiantePeriodoAcademico
    {
        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombrePeriodoAcademico")]
        [StringLength(20)]
        [Required]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;

    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Competencia_Estudiante.Procedimientos
{
    public class CalificarCompetencia
    {
        [JsonPropertyName("idCompetencia")]
        [Required]
        public int idCompetencia { get; set; } = 0;

        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("estadoCompetenciaEstudiante")]
        [StringLength(20)]
        [Required]
        public string estadoCompetenciaEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("idFuncionarioAsignaturaGradoGrupo")]
        [Required]
        public int idFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

    }
}

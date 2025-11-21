using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Competencia.Procedimientos
{
    public class ConsultarCompetenciaAsociadaGradoGrupoEntrada
    {
        [JsonPropertyName("idFuncionarioAsignatuarGradoGrupo")]
        [Required]
        public int idFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

        [JsonPropertyName("idPeriodoAcademico")]
        [Required]
        public int idPeriodoAcademico { get; set; } = 0;
    }
}

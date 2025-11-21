using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario_Asignatura.Procedimientos
{
    public class GestionarEstadoFuncionarioAsignatura
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("idFuncionarioAsignaturaGradoGrupo")]
        [Required]
        public int idFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

    }
}

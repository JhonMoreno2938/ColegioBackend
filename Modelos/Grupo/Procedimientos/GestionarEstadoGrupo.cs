using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Procedimientos
{
    public class GestionarEstadoGrupo
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreGrupo")]
        [StringLength(3)]
        [Required]
        public string nombreGrupo { get; set; } = string.Empty;
    }
}

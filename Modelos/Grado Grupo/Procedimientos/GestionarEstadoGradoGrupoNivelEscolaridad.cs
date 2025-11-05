using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Procedimientos
{
    public class GestionarEstadoGradoGrupoNivelEscolaridad
    {

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        [StringLength(6)]
        [Required]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;
    }
}

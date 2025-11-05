using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Procedimientos
{
    public class RegistrarGradoGrupoNivelEscolaridad
    {
        [JsonPropertyName("nombreGrado")]
        [StringLength(2)]
        [Required]
        public string nombreGrado { get; set; } = string.Empty;

        [JsonPropertyName("nombreGrupo")]
        [StringLength(3)]
        [Required]
        public string nombreGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

    }
}

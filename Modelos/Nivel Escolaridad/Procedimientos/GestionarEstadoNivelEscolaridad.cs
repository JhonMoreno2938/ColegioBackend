using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Procedimientos
{
    public class GestionarEstadoNivelEscolaridad
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;
    }
}

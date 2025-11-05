using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Procedimientos
{
    public class GestionarEstadoGrado
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreGrado")]
        [StringLength(2)]
        [Required]
        public string nombreGrado { get; set; } = string.Empty;
    }
}

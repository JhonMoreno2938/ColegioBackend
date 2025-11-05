using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Procedimientos
{
    public class GestionarEstadoJornada
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada_Sede.Procedimientos
{
    public class GestionarEstadoJornadaSede
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("codigoDane")]
        [StringLength(10)]
        [Required]
        public string codigoDane { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

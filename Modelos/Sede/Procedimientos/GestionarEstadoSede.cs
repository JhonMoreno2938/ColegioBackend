using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Procedimientos
{
    public class GestionarEstadoSede
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede { get; set; } = string.Empty;
    }
}

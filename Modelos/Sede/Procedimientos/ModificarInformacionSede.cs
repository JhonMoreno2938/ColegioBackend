using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Procedimientos
{
    public class ModificarInformacionSede
    {
        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        [Required]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("direccionSede")]
        [StringLength(100)]
        [Required]
        public string direccionSede { get; set; } = string.Empty;

        [JsonPropertyName("numeroContactoSede")]
        [StringLength(10)]
        [Required]
        public string numeroContactoSede { get; set; } = string.Empty;
    }
}

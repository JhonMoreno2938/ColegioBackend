using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Vistas
{
    public class ListarSede
    {
        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        public string codigoDaneSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("estadoSede")]
        [StringLength(10)]
        public string estadoSede { get; set; } = string.Empty;
    }
}

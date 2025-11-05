using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Rh.Vistas
{
    public class ListarRh
    {
        [JsonPropertyName("nombreRh")]
        [StringLength(3)]
        public string nombreRh { get; set; } = string.Empty;
    }
}

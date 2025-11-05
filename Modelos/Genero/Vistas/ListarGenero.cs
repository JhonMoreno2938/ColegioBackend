using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Genero.Vistas
{
    public class ListarGenero
    {
        [JsonPropertyName("nombreGenero")]
        [StringLength(10)]
        public string nombreGenero { get; set; } = string.Empty;
    }
}

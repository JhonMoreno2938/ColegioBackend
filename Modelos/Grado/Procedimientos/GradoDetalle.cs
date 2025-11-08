using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Procedimientos
{
    public class GradoDetalle
    {
        [JsonPropertyName("nombreGrado")]
        public string nombreGrado { get; set; } = string.Empty;
    }
}

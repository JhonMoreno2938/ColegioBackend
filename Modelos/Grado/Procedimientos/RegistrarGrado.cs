using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Procedimientos
{
    public class RegistrarGrado
    {
        [JsonPropertyName("nombreGrado")]
        public string nombreGrado { get; set; } = string.Empty;
    }
}

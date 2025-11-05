using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Procedimientos
{
    public class RegistrarJornada
    {
        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

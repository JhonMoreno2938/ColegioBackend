using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Procedimientos
{
    public class JornadaDetalle
    {

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

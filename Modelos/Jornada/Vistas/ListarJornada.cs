using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Vistas
{
    public class ListarJornada
    {
        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("estadoJornada")]
        public string estadoJornada { get; set; } = string.Empty;
    }
}

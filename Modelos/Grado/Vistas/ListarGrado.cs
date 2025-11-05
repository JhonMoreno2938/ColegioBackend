using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Vistas
{
    public class ListarGrado
    {

        [JsonPropertyName("nombreGrado")]
        public string nombreGrado { get; set; } = string.Empty;

        [JsonPropertyName("estadoGrado")]
        public string estadoGrado { get; set; } = string.Empty;

    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura.Vistas
{
    public class ListarAsignatura
    {
        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("estadoAsignatura")]
        public string estadoAsgiantura { get; set; } = string.Empty;

    }
}

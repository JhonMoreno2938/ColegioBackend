using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante.Vistas
{
    public class ListarEstudiante
    {
        [JsonPropertyName("nombreCompleto")]
        public string nombreCompleto { get; set; } = string.Empty;

        [JsonPropertyName("numeroDocumento")]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("codigoEstudiante")]
        public string codigoEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("estadoEstudiante")]
        public string estadoEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

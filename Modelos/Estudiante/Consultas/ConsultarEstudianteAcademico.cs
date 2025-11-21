using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante.Consultas
{
    public class ConsultarEstudianteAcademico
    {
        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("fechaInscripcionGradoGrupo")]
        public string fechaInscripcionGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("estadoEstudianteSedeJornadaGradoGrupo")]
        public string estadoEstudianteSedeJornadaGradoGrupo { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura_Grado_Grupo.Procedimientos
{
    public class GestionarEstadoAsignaturaGradoGrupo
    {
        [JsonPropertyName("nombreOperacion")]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura.Salidas_Procedimientos
{
    public class SalidaConsultarGradoGrupoAsignaturaEstadoActivo
    {
        public string mensaje { get; set; }

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty; 
    }
}

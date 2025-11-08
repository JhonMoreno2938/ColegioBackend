using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura.Vistas
{
    public class ListarAsiganturaEstadoActivo
    {

        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;
    }
}

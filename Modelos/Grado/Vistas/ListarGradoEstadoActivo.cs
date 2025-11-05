using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Vistas
{
    public class ListarGradoEstadoActivo
    {
        [JsonPropertyName("nombreGrado")]
        public string nombreGrado { get; set; } = string.Empty;
    }
}

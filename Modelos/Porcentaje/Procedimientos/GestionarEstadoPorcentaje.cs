using System.Text.Json.Serialization;

namespace Colegio.Modelos.Porcentaje.Procedimientos
{
    public class GestionarEstadoPorcentaje
    {

        [JsonPropertyName("nombreOperacion")]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;
    }
}

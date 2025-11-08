using System.Text.Json.Serialization;

namespace Colegio.Modelos.Porcentaje.Vistas
{
    public class ListarPorcentaje
    {
        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;

        [JsonPropertyName("estadoPorcentaje")]
        public string estadoPorcentaje { get; set; } = string.Empty;
    }
}

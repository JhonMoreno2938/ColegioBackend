using System.Text.Json.Serialization;

namespace Colegio.Modelos.Porcentaje.Procedimientos
{
    public class RegistrarPorcentaje
    {
        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;
    }
}

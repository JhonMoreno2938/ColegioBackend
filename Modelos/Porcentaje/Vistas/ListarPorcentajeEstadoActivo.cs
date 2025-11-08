using System.Text.Json.Serialization;

namespace Colegio.Modelos.Porcentaje.Vistas
{
    public class ListarPorcentajeEstadoActivo
    {
        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;

    }
}

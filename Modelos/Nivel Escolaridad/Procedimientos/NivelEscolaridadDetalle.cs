using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Procedimientos
{
    public class NivelEscolaridadDetalle
    {
        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;
    }
}

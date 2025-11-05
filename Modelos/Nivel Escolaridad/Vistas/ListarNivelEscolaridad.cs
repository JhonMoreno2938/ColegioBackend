using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Vistas
{
    public class ListarNivelEscolaridad
    {
        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("estadoNivelEscolaridad")]
        public string estadoNivelEscolaridad { get; set; } = string.Empty;

    }
}

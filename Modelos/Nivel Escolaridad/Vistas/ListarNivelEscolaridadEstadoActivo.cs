using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Vistas
{
    public class ListarNivelEscolaridadEstadoActivo
    {
        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;
    }
}

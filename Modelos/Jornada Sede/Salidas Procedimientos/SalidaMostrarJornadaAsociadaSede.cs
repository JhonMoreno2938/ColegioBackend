using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada_Sede.Salidas_Procedimientos
{
    public class SalidaMostrarJornadaAsociadaSede
    {

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("estadoJornadaSede")]
        public string estadoJornadaSede { get; set; } = string.Empty;
    }
}

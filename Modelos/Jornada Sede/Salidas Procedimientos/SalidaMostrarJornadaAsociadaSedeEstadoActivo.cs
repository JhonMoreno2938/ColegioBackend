using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada_Sede.Salidas_Procedimientos
{
    public class SalidaMostrarJornadaAsociadaSedeEstadoActivo
    {

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

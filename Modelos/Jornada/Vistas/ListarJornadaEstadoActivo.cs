using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Vistas
{
    public class ListarJornadaEstadoActivo
    {
        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

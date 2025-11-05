using Colegio.Modelos.Jornada.Procedimientos;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada_Sede.Procedimientos
{
    public class RegistrarJornadaSede
    {
        [JsonPropertyName("codigoDaneSede")]
        public string codigoDaneSede { get; set; } = string.Empty;


        [JsonPropertyName("listaJornada")]
        public List<JornadaDetalle> listaJornada { get; set; } = new List<JornadaDetalle>();
    }
}

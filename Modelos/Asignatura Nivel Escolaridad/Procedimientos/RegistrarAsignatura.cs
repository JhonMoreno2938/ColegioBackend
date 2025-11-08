using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura_Nivel_Escolaridad.Procedimientos
{
    public class RegistrarAsignatura
    {
        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("listaIntensidadHoraria")]
        public List<string> listaIntensidadHoraria { get; set; } = new List<string>();

        [JsonPropertyName("listaNivelEscolaridad")]
        public List<NivelEscolaridadDetalle> listaNivelEscolaridad { get; set; } = new List<NivelEscolaridadDetalle>();
    }
}

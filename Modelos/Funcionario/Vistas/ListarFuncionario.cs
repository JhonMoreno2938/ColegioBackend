using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Vistas
{
    public class ListarFuncionario
    {
        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreCompleto")]
        public string nombreCompleto { get; set; } = string.Empty;

        [JsonPropertyName("numeroDocumento")]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("estadoFuncionario")]
        public string estadoFuncionario { get; set; } = string.Empty;
    }
}

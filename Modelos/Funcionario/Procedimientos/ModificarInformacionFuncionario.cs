using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Procedimientos
{
    public class ModificarInformacionFuncionario
    {
        [JsonPropertyName("primerNombre")]
        public string primerNombre { get; set; } = string.Empty;

        [JsonPropertyName("segundoNombre")]
        public string segundoNombre { get; set; } = string.Empty;

        [JsonPropertyName("primerApellido")]
        public string primerApellido { get; set; } = string.Empty;

        [JsonPropertyName("segundoApellido")]
        public string segundoApellido { get; set; } = string.Empty;

        [JsonPropertyName("numeroDocumento")]
        public string numeroDocumento { get; set; } = string.Empty;
    }
}

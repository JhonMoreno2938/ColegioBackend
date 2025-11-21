using System.Text.Json.Serialization;


namespace Colegio.Modelos.Funcionario.Consultas
{
    public class ConsultarFuncionario
    {
        public bool exito { get; set; } = false;

        public string mensaje { get; set; } = string.Empty;

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

        [JsonPropertyName("nombreTipoDocumento")]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [JsonPropertyName("gradosGruposVinculados")]
        public List<ConsultarFuncionarioAcademico> gradosGruposVinculados { get; set; } = new List<ConsultarFuncionarioAcademico>();
    }
}

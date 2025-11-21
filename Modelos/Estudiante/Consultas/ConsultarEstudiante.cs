using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante.Consultas
{
    public class ConsultarEstudiante
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

        [JsonPropertyName("fechaNacimiento")]
        public string fechaNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("edad")]
        public int edad { get; set; } = 0;

        [JsonPropertyName("nombreTipoDocumento")]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreGenero")]
        public string nombreGenero { get; set; } = string.Empty;

        [JsonPropertyName("departamentoNacimiento")]
        public string departamentoNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("departamentoExpedicion")]
        public string departamentoExpedicion { get; set; } = string.Empty;

        [JsonPropertyName("ciudadNacimiento")]
        public string ciudadNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("ciudadExpedicion")]
        public string ciudadExpedicion { get; set; } = string.Empty;

        [JsonPropertyName("nombreRh")]
        public string nombreRh { get; set; } = string.Empty;

        [JsonPropertyName("codigoEstudiante")]
        public string codigoEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("estadoEstudiante")]
        public string estadoEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("nombreEps")]
        public string nombreEps { get; set; } = string.Empty;

        [JsonPropertyName("nombreEstratoSocial")]
        public string nombreEstratoSocial { get; set; } = string.Empty;

        [JsonPropertyName("nombreDiscapacidad")]
        public string nombreDiscapacidad { get; set; } = string.Empty;

        [JsonPropertyName("nombreSisben")]
        public string nombreSisben { get; set; } = string.Empty;

        [JsonPropertyName("gradosGruposVinculados")]
        public List<ConsultarEstudianteAcademico> gradosGruposVinculados { get; set; } = new List<ConsultarEstudianteAcademico>();

    }
}

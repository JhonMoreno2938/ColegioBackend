using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante.Procedimientos
{
    public class ModificarInformacionEstudiante
    {
        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("primerNombre")]
        [StringLength(100)]
        public string primerNombre { get; set; } = string.Empty;

        [JsonPropertyName("segundoNombre")]
        [StringLength(100)]
        public string? segundoNombre { get; set; } = string.Empty;

        [JsonPropertyName("primerApellido")]
        [StringLength(100)]
        public string primerApellido { get; set; } = string.Empty;

        [JsonPropertyName("segundoApellido")]
        [StringLength(100)]
        public string? segundoApellido { get; set; } = string.Empty;

        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreDepartamentoNacimiento")]
        [StringLength(35)]
        public string? nombreDepartamentoNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("nombreDepartamentoExpedicionDocumento")]
        [StringLength(35)]
        public string? nombreDepartamentoExpedicionDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreCiudadNacimiento")]
        [StringLength(20)]
        public string? nombreCiudadNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("nombreCiudadExpedicionDocumento")]
        [StringLength(20)]
        public string? nombreCiudadExpedicionDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreEPS")]
        [StringLength(100)]
        public string? nombreEps { get; set; } = string.Empty;


    }
}

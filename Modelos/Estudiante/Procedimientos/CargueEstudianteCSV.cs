using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Estudiante.Procedimientos
{
    public class CargueEstudianteCSV
    {
        [JsonPropertyName("primerNombre")]
        [StringLength(100)]
        [Required]
        public string primerNombre { get; set; } = string.Empty;

        [JsonPropertyName("segundoNombre")]
        [StringLength(100)]
        public string? segundoNombre { get; set; } = string.Empty;


        [JsonPropertyName("primerApellido")]
        [StringLength(100)]
        [Required]
        public string primerApellido { get; set; } = string.Empty;

        [JsonPropertyName("segundoApellido")]
        [StringLength(100)]
        public string? segundoApellido { get; set; } = string.Empty;

        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("fechaNacimiento")]
        [StringLength(10)]
        [Required]
        public string fechaNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("edad")]
        public int edad { get; set; } = 0;

        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        [Required]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreGenero")]
        [StringLength(10)]
        [Required]
        public string nombreGenero { get; set; } = string.Empty;
        
        [JsonPropertyName("nombreRh")]
        [StringLength(4)]
        [Required]
        public string nombreRh { get; set; } = string.Empty;

        [JsonPropertyName("codigoEstudiante")]
        [StringLength(100)]
        [Required]
        public string codigoEstudiante { get; set; } = string.Empty;

        [JsonPropertyName("estadoEstudiante")]
        [StringLength(20)]
        [Required]
        public string estadoEstudiante { get; set; } = string.Empty;


        [JsonPropertyName("nombreEps")]
        [StringLength(100)]
        [Required]
        public string nombreEps { get; set; } = string.Empty;

        [JsonPropertyName("nombreEstratoSocial")]
        [StringLength(10)]
        [Required]
        public string nombreEstratoSocial { get; set; } = string.Empty;

        [JsonPropertyName("nombreDiscapacidad")]
        [StringLength(10)]
        [Required]
        public string nombreDiscapacidad { get; set; } = string.Empty;

        [JsonPropertyName("nombreSisben")]
        [StringLength(10)]
        [Required]
        public string nombreSisben { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        [Required]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("nombreGrado")]
        [StringLength(2)]
        public string nombreGrado { get; set; } = string.Empty;

        [JsonPropertyName("nombreGrupo")]
        [StringLength(4)]
        public string nombreGrupo { get; set; } = string.Empty;

        [JsonPropertyName("annioActual")]
        public int annioActual { get; set; } = 0;

    }
}

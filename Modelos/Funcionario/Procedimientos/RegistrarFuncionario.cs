using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Procedimientos
{
    public class RegistrarFuncionario
    {

        [JsonPropertyName("primerNombre")]
        [StringLength(100)]
        [Required]
        public string primerNombre { get; set; } = string.Empty;

        [JsonPropertyName("segundoNombre")]
        [StringLength(100)]
        public string? segundoNombre { get; set; }

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

        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        [Required]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreGenero")]
        [StringLength(10)]
        [Required]
        public string nombreGenero { get; set; } = string.Empty;

        [JsonPropertyName("nombreTipoFuncionario")]
        [StringLength(20)]
        [Required]
        public string nombreTipoFuncionario { get; set; } = string.Empty;

    }
}

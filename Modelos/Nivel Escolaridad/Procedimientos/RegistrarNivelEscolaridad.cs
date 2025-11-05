using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Procedimientos
{
    public class RegistrarNivelEscolaridad
    {
        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

    }
}

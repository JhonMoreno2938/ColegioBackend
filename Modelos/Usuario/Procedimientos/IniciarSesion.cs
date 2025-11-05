using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Usuario.Procedimientos
{
    public class IniciarSesion
    {
        [JsonPropertyName("nombreUsuario")]
        [Required]
        [StringLength(10)]
        public string nombreUsuario { get; set; } = string.Empty;

        [JsonPropertyName("contraseinaUsuario")]
        [Required]
        [StringLength(255)]
        public string contraseinaUsuario { get; set; } = string.Empty;
    }
}

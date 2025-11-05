using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Rol_Usuario.Vistas
{
    public class ListarRolUsuario
    {
        [JsonPropertyName("nombreRolUsuario")]
        [StringLength(35)]
        public string nombreRolUsuario { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Procedimientos
{
    public class RegistrarGrupo
    {
        [JsonPropertyName("nombre_grupo")]
        [StringLength(4)]
        public string nombreGrupo { get; set; } = string.Empty;
    }
}

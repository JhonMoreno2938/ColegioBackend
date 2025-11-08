using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Procedimientos
{
    public class GrupoDetalle
    {
        [JsonPropertyName("nombreGrupo")]
        public string nombreGrupo { get; set; } = string.Empty;
    }
}

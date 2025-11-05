using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Vistas
{
    public class ListarGrupo
    {
        [JsonPropertyName("nombre_grupo")]
        public string nombreGrupo { get; set; } = string.Empty;

        [JsonPropertyName("estado_grupo")]
        public string estadoGrupo { get; set; } = string.Empty;
    }
}

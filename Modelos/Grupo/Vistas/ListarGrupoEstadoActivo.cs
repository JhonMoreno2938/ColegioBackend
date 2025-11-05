using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Vistas
{
    public class ListarGrupoEstadoActivo
    {
        [JsonPropertyName("nombre_grupo")]
        public string nombreGrupo { get; set; } = string.Empty;
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Procedimientos
{
    public class GradoGrupoDetalle
    {
        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;
    }
}

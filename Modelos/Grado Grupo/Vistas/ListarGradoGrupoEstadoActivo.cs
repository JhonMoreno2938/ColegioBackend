using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Vistas
{
    public class ListarGradoGrupoEstadoActivo
    {
        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

    }
}

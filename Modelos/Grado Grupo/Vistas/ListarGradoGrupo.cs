using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Vistas
{
    public class ListarGradoGrupo
    {
        
        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("estadoGradoGrupo")]
        public string estadoGradoGrupo { get; set; } = string.Empty;

    }
}

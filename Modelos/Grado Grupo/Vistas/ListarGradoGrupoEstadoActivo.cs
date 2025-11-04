using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Vistas
{
    public class ListarGradoGrupoEstadoActivo
    {
        [JsonIgnore]
        GradoGrupoModelo gradoGrupoModelo = new GradoGrupoModelo();

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad
        {
            get => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad;
            set => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad = value;
        }

    }
}

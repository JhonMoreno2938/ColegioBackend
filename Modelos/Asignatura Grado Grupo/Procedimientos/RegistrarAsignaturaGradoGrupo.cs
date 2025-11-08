using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura_Grado_Grupo.Procedimientos
{
    public class RegistrarAsignaturaGradoGrupo
    {
        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("listaGradoGrupo")]
        public List<GradoGrupoDetalle> listaGradoGrupo { get; set; } = new List<GradoGrupoDetalle>();

        [JsonPropertyName("listaNivelEscolaridad")]
        public List<NivelEscolaridadDetalle> listaNivelEscolaridad { get; set; } = new List<NivelEscolaridadDetalle>();
    }
}

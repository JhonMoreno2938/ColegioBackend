using Colegio.Modelos.Grado.Procedimientos;
using Colegio.Modelos.Grupo.Procedimientos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede_Jornada_Grado_Grupo.Procedimientos
{
    public class RegistrarGradoGrupoNivelEscolaridadSede
    {
        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("listaGrado")]
        public List<GradoDetalle> listaGrado { get; set; } = new List<GradoDetalle>();

        [JsonPropertyName("listaGrupo")]
        public List<GrupoDetalle> listaGrupo { get; set; } = new List<GrupoDetalle>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede_Jornada_Grado_Grupo.Procedimientos
{
    public class GestionarEstadoGradoGrupoSede
    {
        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        [StringLength(6)]
        [Required]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

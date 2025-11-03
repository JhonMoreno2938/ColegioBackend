using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos
{
    public class GradoGrupoDetalle
    {
        [JsonPropertyName("nombreGradoGrupo")]
        [StringLength(10)]
        public string NombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        public string NombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        public string NombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("estadoSedeJornadaGradoGrupo")]
        [StringLength(10)]
        public string EstadoSedeJornadaGradoGrupo { get; set; } = string.Empty;
    }
}

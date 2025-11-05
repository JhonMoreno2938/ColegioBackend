using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Salidas_Procedimientos
{
    public class SalidaConsultarSede
    {
        public bool exito { get; set; }

        public string mensaje { get; set; }

        // Información de la sede a consultar.

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        public string codigoDaneSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("direccionSede")]
        [StringLength(100)]
        public string direccionSede { get; set; } = string.Empty;

        [JsonPropertyName("numeroContactoSede")]
        [StringLength(10)]
        public string numeroContactoSede { get; set; } = string.Empty;

        [JsonPropertyName("estadoSede")]
        [StringLength(10)]
        public string estadoSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreTipoSede")]
        [StringLength(15)]
        public string nombreTipoSede { get; set; } = string.Empty;

        [JsonPropertyName("gradosGruposVinculados")]
        public List<GradoGrupoDetalle> GradosGruposVinculados { get; set; } = new List<GradoGrupoDetalle>();
    }
}

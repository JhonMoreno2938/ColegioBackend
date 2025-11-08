using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura.Salidas_Procedimientos
{
    public class InformacionConsultarAsignatura
    {
        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [JsonPropertyName("intensidadHoraria")]
        public int intensidadHoraria { get; set; } = 0;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("estadoAsignaturaGradoGrupo")]
        public string estadoAsignaturaGradoGrupo { get; set; } = string.Empty;
    }
}

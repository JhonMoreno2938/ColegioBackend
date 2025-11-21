using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Procedimientos
{
    public class ConsultarCompetenciaFuncionario
    {
        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("idCompetencia")]
        public int idCompetencia { get; set; } = 0;

        [JsonPropertyName("descripcionCompetencia")]
        public string descripcionCompetencia { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombrePeriodoAcademico")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Competencia.Procedimientos
{
    public class ConsultarCompetenciaAsociadaGradoGrupoSalida
    {
        [JsonPropertyName("idCompetencia")]
        public int idCompetencia { get; set; } = 0;

        [JsonPropertyName("descripcionCompetencia")]
        public string descripcionCompetencia { get; set; } = string.Empty;
    }
}

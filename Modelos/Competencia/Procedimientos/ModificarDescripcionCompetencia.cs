using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Competencia.Procedimientos
{
    public class ModificarDescripcionCompetencia
    {
        [JsonPropertyName("idCompetencia")]
        [Required]
        public int idCompetencia { get; set; } = 0;

        [JsonPropertyName("descripcionCompetencia")]
        [Required]
        public string descripcionCompetencia { get; set; } = string.Empty;
    }
}

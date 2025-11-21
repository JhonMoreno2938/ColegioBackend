using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Calificacion_Academica.Procedimientos
{
    public class RegistrarTipoCalificacionAcademica
    {
        [JsonPropertyName("nombreTipoCalificacionAcademica")]
        [StringLength(20)]
        [Required]
        public string nombreTipoCalificacionAcademica { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        [Required]
        public int valorPorcentaje { get; set; } = 0;
    }
}

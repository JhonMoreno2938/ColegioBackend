using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Procedimientos
{
    public class GestionarEstadoNivelEscolaridad
    {
        [JsonIgnore]
        NivelEscolaridadModelo nivelEscolaridadModelo = new NivelEscolaridadModelo ();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad
        {
            get => nivelEscolaridadModelo.nombreNivelEscolaridad;
            set => nivelEscolaridadModelo.nombreNivelEscolaridad = value;
        }
    }
}

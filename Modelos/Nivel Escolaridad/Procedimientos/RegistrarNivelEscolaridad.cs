using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Procedimientos
{
    public class RegistrarNivelEscolaridad
    {
        [JsonIgnore]
        NivelEscolaridadModelo nivelEscolaridadModelo = new NivelEscolaridadModelo();

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

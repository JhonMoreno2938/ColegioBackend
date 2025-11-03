using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Procedimientos
{
    public class GestionarEstadoGrado
    {
        [JsonIgnore]
        GradoModelo gradoModelo = new GradoModelo();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("nombreGrado")]
        [StringLength(2)]
        [Required]
        public string nombreGrado
        {
            get => gradoModelo.nombreGrado;
            set => gradoModelo.nombreGrado = value;
        }
    }
}

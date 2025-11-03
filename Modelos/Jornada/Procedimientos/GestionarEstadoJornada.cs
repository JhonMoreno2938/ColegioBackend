using Colegio.Modelos.Grupo;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Procedimientos
{
    public class GestionarEstadoJornada
    {
        [JsonIgnore]
        JornadaModelo jornadaModelo = new JornadaModelo();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("nombreJornada")]
        [StringLength(15)]
        [Required]
        public string nombreJornada
        {
            get => jornadaModelo.nombreJornada;
            set => jornadaModelo.nombreJornada = value;
        }
    }
}

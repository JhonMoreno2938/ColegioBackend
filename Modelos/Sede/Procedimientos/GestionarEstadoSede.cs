using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Procedimientos
{
    public class GestionarEstadoSede
    {
        [JsonIgnore]
        SedeModelo sedeModelo = new SedeModelo();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede
        {
            get => sedeModelo.codigoDaneSede;
            set => sedeModelo.codigoDaneSede = value;
        }
    }
}

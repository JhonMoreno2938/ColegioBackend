using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Procedimientos
{
    public class ModificarInformacionSede
    {
        [JsonIgnore]
        SedeModelo sedeModelo = new SedeModelo();

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        [Required]
        public string codigoDaneSede
        {
            get => sedeModelo.codigoDaneSede;
            set => sedeModelo.codigoDaneSede = value;
        }

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        [Required]
        public string nombreSede
        {
            get => sedeModelo.nombreSede;
            set => sedeModelo.nombreSede = value;
        }

        [JsonPropertyName("direccionSede")]
        [StringLength(100)]
        [Required]
        public string direccionSede
        {
            get => sedeModelo.direccionSede;
            set => sedeModelo.direccionSede = value;
        }

        [JsonPropertyName("numeroContactoSede")]
        [StringLength(10)]
        [Required]
        public string numeroContactoSede
        {
            get => sedeModelo.numeroContactoSede;
            set => sedeModelo.numeroContactoSede = value;
        }
    }
}

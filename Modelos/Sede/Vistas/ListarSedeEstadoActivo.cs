using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Vistas
{
    public class ListarSedeEstadoActivo
    {
        [JsonIgnore]
        SedeModelo sedeModelo = new SedeModelo();

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        public string codigoDaneSede
        {
            get => sedeModelo.codigoDaneSede;
            set => sedeModelo.codigoDaneSede = value;
        }

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        public string nombreSede
        {
            get => sedeModelo.nombreSede;
            set => sedeModelo.nombreSede = value;
        }
    }
}

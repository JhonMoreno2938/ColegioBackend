using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Rh.Vistas
{
    public class ListarRh
    {
        [JsonIgnore]
        private readonly RhModelo rhModelo = new RhModelo();

        [JsonPropertyName("nombreRh")]
        [StringLength(3)]
        public string nombreRh
        {
            get => rhModelo.nombreRh;
            set => rhModelo.nombreRh = value;
        }
    }
}

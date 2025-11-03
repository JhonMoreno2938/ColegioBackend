using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Genero.Vistas
{
    public class ListarGenero
    {
        [JsonIgnore]
        private readonly GeneroModelo generoModelo = new GeneroModelo();

        [JsonPropertyName("nombreGenero")]
        [StringLength(10)]
        public string nombreGenero
        {
            get => generoModelo.nombreGenero;
            set => generoModelo.nombreGenero = value;
        }

    }
}

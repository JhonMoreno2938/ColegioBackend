using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Procedimientos
{
    public class RegistrarGrado
    {
        [JsonIgnore]
        GradoModelo gradoModelo = new GradoModelo();

        [JsonPropertyName("nombreGrado")]
        public string nombreGrado
        {
            get => gradoModelo.nombreGrado;
            set => gradoModelo.nombreGrado = value;
        }
    }
}

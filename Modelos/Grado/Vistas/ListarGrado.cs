using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Vistas
{
    public class ListarGrado
    {
        [JsonIgnore]
        GradoModelo gradoModelo = new GradoModelo();

        [JsonPropertyName("nombreGrado")]
        public string nombreGrado
        {
            get => gradoModelo.nombreGrado;
            set => gradoModelo.nombreGrado = value;
        }

        [JsonPropertyName("estadoGrado")]
        public string estadoGrado
        {
            get => gradoModelo.estadoGrado;
            set => gradoModelo.estadoGrado = value;
        }

    }
}

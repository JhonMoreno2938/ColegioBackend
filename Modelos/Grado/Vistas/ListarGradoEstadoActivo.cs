using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado.Vistas
{
    public class ListarGradoEstadoActivo
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

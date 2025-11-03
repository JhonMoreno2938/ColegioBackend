using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Procedimientos
{
    public class RegistrarJornada
    {
        [JsonIgnore]
        JornadaModelo jornadaModelo = new JornadaModelo();

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada
        {
            get => jornadaModelo.nombreJornada;
            set => jornadaModelo.nombreJornada = value;
        }

    }
}

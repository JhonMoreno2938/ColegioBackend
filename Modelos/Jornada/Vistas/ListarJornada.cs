using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Vistas
{
    public class ListarJornada
    {
        [JsonIgnore]
        JornadaModelo jornadaModelo = new JornadaModelo();

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada
        {
            get => jornadaModelo.nombreJornada;
            set => jornadaModelo.nombreJornada = value;
        }

        [JsonPropertyName("estadoJornada")]
        public string estadoJornada
        {
            get => jornadaModelo.estadoJornada;
            set => jornadaModelo.estadoJornada = value;
        }
    }
}

using System.Text.Json.Serialization;

namespace Colegio.Modelos.Jornada.Vistas
{
    public class ListarJornadaEstadoActivo
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

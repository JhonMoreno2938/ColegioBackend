using System.Text.Json.Serialization;

namespace Colegio.Modelos.Nivel_Escolaridad.Vistas
{
    public class ListarNivelEscolaridad
    {
        [JsonIgnore]
        NivelEscolaridadModelo nivelEscolaridadModelo = new NivelEscolaridadModelo();

        [JsonPropertyName("nombreNivelEscolaridad")]
        public string nombreNivelEscolaridad
        {
            get => nivelEscolaridadModelo.nombreNivelEscolaridad;
            set => nivelEscolaridadModelo.nombreNivelEscolaridad = value;
        }

        [JsonPropertyName("estadoNivelEscolaridad")]
        public string estadoNivelEscolaridad
        {
            get => nivelEscolaridadModelo.estadoNivelEscolaridad;
            set => nivelEscolaridadModelo.estadoNivelEscolaridad = value;
        }

    }
}

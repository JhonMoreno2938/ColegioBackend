using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Calificacion_Academica.Vistas
{
    public class ListarTipoCalificacionAcademica
    {
        [JsonPropertyName("nombreTipoCalificacionAcademica")]
        public string nombreTipoCalificacionAcademica { get; set; } = string.Empty;

        [JsonPropertyName("valorPorcentaje")]
        public int valorPorcentaje { get; set; } = 0;
    }
}

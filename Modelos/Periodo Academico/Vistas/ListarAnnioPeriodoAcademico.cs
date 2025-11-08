using System.Text.Json.Serialization;

namespace Colegio.Modelos.Periodo_Academico.Vistas
{
    public class ListarAnnioPeriodoAcademico
    {
        [JsonPropertyName("annioPeriodoAcademico")]
        public int annioPeriodoAcademico { get; set; } = 0;
    }
}

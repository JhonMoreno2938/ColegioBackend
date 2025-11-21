using Colegio.Utilidades;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario_Periodo_Academico.Procedimientos
{
    public class ActualizarFechaHabilitacionFuncionarioPeriodoAcademico
    {
        [JsonPropertyName("listaNumeroDocumento")]
        [Required]
        public List<NumeroDocumentoDto> listaNumeroDocumento { get; set; } = new List<NumeroDocumentoDto>();

        [JsonPropertyName("fechaInicioHabilitacion")]
        [Required]
        public string fechaInicioHabilitacion { get; set; } = string.Empty;

        [JsonPropertyName("fechaFinalHabilitacion")]
        [Required]
        public string fechaFinalHabilitacion { get; set; } = string.Empty;

        [JsonPropertyName("idPeriodoAcademico")]
        [Required]
        public int idPeriodoAcademico { get; set; } = 0;
    }
}

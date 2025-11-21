using Colegio.Utilidades;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario_Asignatura.Procedimientos
{
    public class AsignarFuncionarioAsignatura
    {
        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento { get; set; } = string.Empty;

        [JsonPropertyName("nombreAsignatura")]
        [StringLength(100)]
        [Required]
        public string nombreAsignatura { get; set; } = string.Empty;


        [JsonPropertyName("listaGradoGrupoJornada")]
        public List<GradoGrupoJornadaDto> listaGradoGrupoJornada { get; set; } = new List<GradoGrupoJornadaDto>();
    }
}

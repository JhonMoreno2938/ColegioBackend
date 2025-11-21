using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Procedimientos
{
    public class ConsultarGradoGrupoFuncionarioEstadoActivo
    {

        [JsonPropertyName("idFuncionarioAsignaturaGradoGrupo")]
        public int idFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;
    }
}

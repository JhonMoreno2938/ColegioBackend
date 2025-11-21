using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Consultas
{
    public class ConsultarFuncionarioAcademico
    {
        [JsonPropertyName("idFuncionarioAsignaturaGradoGrupo")]
        public int idFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

        [JsonPropertyName("estadoFuncionarioAsignaturaGradoGrupo")]
        public string estadoFuncionarioAsignaturaGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreAsignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [JsonPropertyName("nombreGradoGrupo")]
        public string nombreGradoGrupo { get; set; } = string.Empty;

        [JsonPropertyName("nombreSede")]
        public string nombreSede { get; set; } = string.Empty;

        [JsonPropertyName("nombreJornada")]
        public string nombreJornada { get; set; } = string.Empty;
    }
}

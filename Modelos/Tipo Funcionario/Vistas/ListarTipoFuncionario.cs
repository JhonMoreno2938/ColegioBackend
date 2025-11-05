using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Funcionario.Vistas
{
    public class ListarTipoFuncionario
    {
        [JsonPropertyName("nombreTipoFuncionario")]
        [StringLength(20)]
        public string nombreTipoFuncionario { get; set; } = string.Empty;
    }
}

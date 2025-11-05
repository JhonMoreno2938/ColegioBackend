using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Departamento.Vistas
{
    public class ListarDepartamento
    {

        [JsonPropertyName("nombreDepartamento")]
        [StringLength(35)]
        public string nombreDepartamento { get; set; } = string.Empty;
    }
}

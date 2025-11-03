using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Departamento.Vistas
{
    public class ListarDepartamento
    {
        [JsonIgnore]
        private readonly DepartamentoModelo departamentoModelo = new DepartamentoModelo();

        [JsonPropertyName("nombreDepartamento")]
        [StringLength(35)]
        public string nombreDepartamento
        {
            get => departamentoModelo.nombreDepartamento;
            set => departamentoModelo.nombreDepartamento = value;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Funcionario.Vistas
{
    public class ListarTipoFuncionario
    {
        [JsonIgnore]
        private readonly TipoFuncionarioModelo tipoFuncionarioModelo = new TipoFuncionarioModelo();

        [JsonPropertyName("nombreTipoFuncionario")]
        [StringLength(20)]
        public string nombreTipoFuncionario
        {
            get => tipoFuncionarioModelo.nombreTipoFuncionario;
            set => tipoFuncionarioModelo.nombreTipoFuncionario = value;
        }
    }
}

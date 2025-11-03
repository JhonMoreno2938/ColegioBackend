using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Tipo_Funcionario
{
    [Table("tipo_funcionario")]
    public class TipoFuncionarioModelo
    {
        [Key]
        [Column("pk_id_tipo_funcionario")]
        public int pkIdTipoFuncionario { get; set; } = 0;

        [Column("nombre_tipo_funcionario")]
        public string nombreTipoFuncionario { get; set; } = string.Empty;

        [Column("estado_tipo_funcionario")]
        public string estadoTipoFuncionario { get; set; } = string.Empty;
        public TipoFuncionarioModelo()
        {

        }
    }
}

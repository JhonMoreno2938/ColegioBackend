using Colegio.Modelos.Persona;
using Colegio.Modelos.Tipo_Funcionario;
using Colegio.Modelos.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Funcionario
{
    [Table("funcionario_modelo")]
    public class FuncionarioModelo
    {
        public PersonaModelo personaModelo { get; set; } = new PersonaModelo();
        public TipoFuncionarioModelo tipoFuncionarioModelo { get; set; } = new TipoFuncionarioModelo();
        public UsuarioModelo usuarioModelo { get; set; } = new UsuarioModelo();

        [Key]
        [Column("pk_id_funcionario")]
        public int pkIdFuncionario { get; set; } = 0;

        [Column("intensidad_horaria_funcionario")]
        public int? intensidadHorariaFuncionario { get; set; } = 0;

        [Column("estado_funcionario")]
        public string estadoFuncionario { get; set; } = string.Empty;

        [Column("fk_id_persona")]
        public int fkIdPersona
        {
            get => personaModelo.pkIdPersona;
            set => personaModelo.pkIdPersona = value;
        }

        [Column("fk_id_tipo_funcionario")]
        public int fkIdTipoFuncionario
        {
            get => tipoFuncionarioModelo.pkIdTipoFuncionario;
            set => tipoFuncionarioModelo.pkIdTipoFuncionario = value;
        }

        [Column("fk_id_usuario")]
        public int fkIdUsuario
        {
            get => usuarioModelo.pkIdUsuario;
            set => usuarioModelo.pkIdUsuario = value;
        }
        public FuncionarioModelo()
        {

        }

    }
}

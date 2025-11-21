using Colegio.Modelos.Asignatura;
using Colegio.Modelos.Funcionario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Funcionario_Asignatura
{
    [Table("funcionario_asignatura")]
    public class FuncionarioAsignaturaModelo
    {
        public AsignaturaModelo asignaturaModelo { get; set; } = new AsignaturaModelo();
        public FuncionarioModelo funcionarioModelo { get; set; } = new FuncionarioModelo();


        [Key]
        [Column("pk_id_funcionario_asignatura")]
        public int pkIdFuncionarioAsignatura { get; set; } = 0;

        [Column("estado_funcionario_asignatura")]
        public string estadoFuncionarioAsignatura { get; set; } = string.Empty;

        [Column("fk_id_asignatura")]
        public int fkIdAsignatura
        {
            get => asignaturaModelo.pkIdAsignatura;
            set => asignaturaModelo.pkIdAsignatura = value;
        }

        [Column("fk_id_funcionario")]
        public int fkIdFuncionario
        {
            get => funcionarioModelo.pkIdFuncionario;
            set => funcionarioModelo.pkIdFuncionario = value;
        }

    }
}

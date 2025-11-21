using Colegio.Modelos.Funcionario_Asignatura;
using Colegio.Modelos.Sede_Jornada_Grado_Grupo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Funcionario_Asignatura_Grado_Grupo
{
    [Table("funcionario_asignatura_grado_grupo")]
    public class FuncionarioAsignaturaGradoGrupoModelo
    {
        public FuncionarioAsignaturaModelo funcionarioAsignaturaModelo { get; set; } = new FuncionarioAsignaturaModelo();
        public SedeJornadaGradoGrupoModelo sedeJornadaGradoGrupoModelo { get; set; } = new SedeJornadaGradoGrupoModelo();

        [Key]
        [Column("pk_id_funcionario_asignatura_grado_grupo")]
        public int pkIdFuncionarioAsignaturaGradoGrupo { get; set; } = 0;

        [Column("estado_funcionario_asignatura_grado_grupo")]
        public string estadoFuncionarioAsignaturaGradoGrupo { get; set; } = string.Empty;

        [Column("fk_id_funcionario_asignatura")]
        public int fkIdFuncionarioAsignatura
        {
            get => funcionarioAsignaturaModelo.pkIdFuncionarioAsignatura;
            set => funcionarioAsignaturaModelo.pkIdFuncionarioAsignatura = value;
        }

        [Column("fk_id_sede_jornada_grado_grupo")]
        public int fkIdSedeJornadaGradoGrupo
        {
            get => sedeJornadaGradoGrupoModelo.pkIdSedeJornadaGradoGrupo;
            set => sedeJornadaGradoGrupoModelo.pkIdSedeJornadaGradoGrupo = value;
        }



    }
}

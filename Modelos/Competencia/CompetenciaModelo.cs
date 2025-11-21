using Colegio.Modelos.Funcionario_Asignatura_Grado_Grupo;
using Colegio.Modelos.Periodo_Academico;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Competencia
{
    [Table("competencia")]
    public class CompetenciaModelo
    {
        public FuncionarioAsignaturaGradoGrupoModelo funcionarioAsignaturaGradoGrupoModelo { get; set; } = new FuncionarioAsignaturaGradoGrupoModelo();
        public PeriodoAcademicoModelo periodoAcademicoModelo { get; set; } = new PeriodoAcademicoModelo();

        [Key]
        [Column("pk_id_competencia")]
        public int pkIdCompetencia { get; set; } = 0;

        [Column("descripcion_competencia")]
        public string descripcionCompetencia { get; set; } = string.Empty;

        [Column("fk_id_funcionario_asignatura_grado_grupo")]
        public int fkIdFuncionarioAsignaturaGradoGrupo
        {
            get => funcionarioAsignaturaGradoGrupoModelo.pkIdFuncionarioAsignaturaGradoGrupo;
            set => funcionarioAsignaturaGradoGrupoModelo.pkIdFuncionarioAsignaturaGradoGrupo = value;
        }

        [Column("fk_id_periodo_academico")]
        public int fkIdPeriodoACademico
        {
            get => periodoAcademicoModelo.pkIdPeriodoAcademico;
            set => periodoAcademicoModelo.pkIdPeriodoAcademico = value;
        }
    }
}

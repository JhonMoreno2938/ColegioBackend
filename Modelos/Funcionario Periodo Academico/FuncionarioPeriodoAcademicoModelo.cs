using Colegio.Modelos.Funcionario_Asignatura_Grado_Grupo;
using Colegio.Modelos.Periodo_Academico;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Funcionario_Periodo_Academico
{
    [Table("funcionario_periodo_academico")]
    public class FuncionarioPeriodoAcademicoModelo
    {
        public PeriodoAcademicoModelo periodoAcademicoModelo { get; set; } = new PeriodoAcademicoModelo();
        public FuncionarioAsignaturaGradoGrupoModelo FuncionarioAsignaturaGradoGrupoModelo { get; set; } = new FuncionarioAsignaturaGradoGrupoModelo();

        [Key]
        [Column("pk_id_funcionario_periodo_academico")]
        public int pkIdFuncionarioPeriodoAcademico { get; set; } = 0;

        [Column("fecha_inicio_habilitacion")]
        public string fechaInicioHabilitacion { get; set; } = string.Empty;

        [Column("fecha_final_habilitacion")]
        public string fechaFinalHabilitacion { get; set; } = string.Empty;



    }
}

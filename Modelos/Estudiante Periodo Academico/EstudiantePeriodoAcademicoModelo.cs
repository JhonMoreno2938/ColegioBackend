using Colegio.Modelos.Estudiante_Sede_Jornada_Grado_Grupo;
using Colegio.Modelos.Periodo_Academico;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Estudiante_Periodo_Academico
{
    [Table("estudiante_periodo_academico")]
    public class EstudiantePeriodoAcademicoModelo
    {
        public EstudianteSedeJornadaGradoGrupoModelo estudianteSedeJornadaGradoGrupoModelo { get; set; } = new EstudianteSedeJornadaGradoGrupoModelo();
        public PeriodoAcademicoModelo periodoAcademicoModelo { get; set; } = new PeriodoAcademicoModelo();

        [Key]
        [Column("pk_id_estudiante_periodo_academico")]
        public int pkIdEstudiantePeriodoAcademico { get; set; } = 0;

        [Column("estado_estudiante_periodo_academico")]
        public string estadoEstudiantePeriodoAcademico { get; set; } = string.Empty;

        [Column("fk_id_estudiante_sede_jornada_grado_grupo")]
        public int fkIdEstudianteSedeJornadaGradoGrupo
        {
            get => estudianteSedeJornadaGradoGrupoModelo.pkIdEstudianteSedeJornadaGradoGrupo;
            set => estudianteSedeJornadaGradoGrupoModelo.pkIdEstudianteSedeJornadaGradoGrupo = value;
        }

        [Column("fk_id_periodo_academico")]
        public int fkIdPeriodoAcademico
        {
            get => periodoAcademicoModelo.pkIdPeriodoAcademico;
            set => periodoAcademicoModelo.pkIdPeriodoAcademico = value;
        }
    }
}

using Colegio.Modelos.Competencia;
using Colegio.Modelos.Estudiante_Sede_Jornada_Grado_Grupo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Competencia_Estudiante
{
    [Table("competencia_estudiante")]
    public class CompetenciaEstudianteModelo
    {
        public CompetenciaModelo competenciaModelo { get; set; } = new CompetenciaModelo();
        public EstudianteSedeJornadaGradoGrupoModelo estudianteSedeJornadaGradoGrupoModelo { get; set; } = new EstudianteSedeJornadaGradoGrupoModelo();

        [Key]
        [Column("pk_id_competencia_estudiante")]
        public int pkIdCompetenciaEstudiante { get; set; } = 0;

        [Column("estado_competencia_estudiante")]
        public string estadoCompetenciaEstudiante { get; set; } = string.Empty;

        [Column("fk_id_competencia")]
        public int fkIdCompetencia
        {
            get => competenciaModelo.pkIdCompetencia;
            set => competenciaModelo.pkIdCompetencia = value;
        }

        [Column("fk_id_estudiante_sede_jornada_grado_grupo")]
        public int fkIdEstudianteSedeJornadaGradoGrupo
        {
            get => estudianteSedeJornadaGradoGrupoModelo.pkIdEstudianteSedeJornadaGradoGrupo;
            set => estudianteSedeJornadaGradoGrupoModelo.pkIdEstudianteSedeJornadaGradoGrupo = value;
        }
    }
}

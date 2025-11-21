using Colegio.Modelos.Estudiante;
using Colegio.Modelos.Sede_Jornada_Grado_Grupo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Estudiante_Sede_Jornada_Grado_Grupo
{
    [Table("estudiante_sede_jornada_grado_grupo")]
    public class EstudianteSedeJornadaGradoGrupoModelo
    {
        public SedeJornadaGradoGrupoModelo sedeJornadaGradoGrupoModelo { get; set; } = new SedeJornadaGradoGrupoModelo();
        public EstudianteModelo estudianteModelo { get; set; } = new EstudianteModelo();

        [Key]
        [Column("pk_id_estudiante_sede_jornada_grado_grupo")]
        public int pkIdEstudianteSedeJornadaGradoGrupo { get; set; } = 0;

        [Column("fecha_inscripcion_grado_grupo")]
        public string fechaInscripcionGradoGrupo { get; set; } = string.Empty;

        [Column("estado_estudiante_sede_jornada_grado_grupo")]
        public string estadoEstudianteSedeJornadaGradoGrupo { get; set; } = string.Empty;

        [Column("fk_id_sede_jornada_grado_grupo")]
        public int fkIdSedeJornadaGradoGrupo
        {
            get => sedeJornadaGradoGrupoModelo.pkIdSedeJornadaGradoGrupo;
            set => sedeJornadaGradoGrupoModelo.pkIdSedeJornadaGradoGrupo = value;
        }

        [Column("fk_id_estudiante")]
        public int fkIdEstudiante
        {
            get => estudianteModelo.pkIdEstudiante;
            set => estudianteModelo.pkIdEstudiante = value;
        }
    }
}

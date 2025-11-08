using Colegio.Modelos.Asignatura_Nivel_Escolaridad;
using Colegio.Modelos.Grado_Grupo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Asignatura_Grado_Grupo
{
    [Table("asignatura_grado_grupo")]
    public class AsignaturaGradoGrupoModelo
    {
        public AsignaturaNivelEscolaridadModelo asignaturaNivelEscolaridadModelo { get; set; } = new AsignaturaNivelEscolaridadModelo();
        public GradoGrupoModelo gradoGrupoModelo { get; set; } = new GradoGrupoModelo();

        [Key]
        [Column("pk_id_asignatura_grado_grupo")]
        public int pkIdAsignaturaGradoGrupo { get; set; } = 0;

        [Column("estado_asignatura_grado_grupo")]
        public string estadoAsignaturaGradoGrupo { get; set; } = string.Empty;

        [Column("fk_id_asignatura_nivel_escolaridad")]
        public int fkIdAsignaturaNivelEscolaridad
        {
            get => asignaturaNivelEscolaridadModelo.pkIdAsignaturaNivelEscolaridad;
            set => asignaturaNivelEscolaridadModelo.pkIdAsignaturaNivelEscolaridad = value;
        }

        [Column("fk_id_grado_grupo")]
        public int fkIdGradoGrupo
        {
            get => gradoGrupoModelo.pkIdGradoGrupo;
            set => gradoGrupoModelo.pkIdGradoGrupo = value;
        }
    }
}

using Colegio.Modelos.Asignatura;
using Colegio.Modelos.Nivel_Escolaridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Asignatura_Nivel_Escolaridad
{
    [Table("asignatura_nivel_escolaridad")]
    public class AsignaturaNivelEscolaridadModelo
    {
        public NivelEscolaridadModelo nivelEscolaridadModelo { get; set; } = new NivelEscolaridadModelo();
        public AsignaturaModelo asignaturaModelo { get; set; } = new AsignaturaModelo();

        [Key]
        [Column("pk_id_asignatura_nivel_escolaridad")]
        public int pkIdAsignaturaNivelEscolaridad { get; set; } = 0;

        [Column("intensidad_horaria")]
        public int? intensidadHoraria { get; set; } = 0;

        [Column("estado_asignatura_nivel_escolariadad")]
        public string esstadoAsignaturaNivelEscolaridad { get; set; } = string.Empty;

        [Column("fk_id_asignatura")]
        public int fkIdAsignatura
        {
            get => asignaturaModelo.pkIdAsignatura;
            set => asignaturaModelo.pkIdAsignatura = value;
        }

        [Column("fk_id_nivel_escolaridad")]
        public int fkIdNivelEscolaridad
        {
            get => nivelEscolaridadModelo.pkIdNivelEscolaridad;
            set => nivelEscolaridadModelo.pkIdNivelEscolaridad = value;
        }

    }
}

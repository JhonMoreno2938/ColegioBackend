using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Nombre_Periodo_Academico
{
    [Table("nombre_periodo_academico")]
    public class NombrePeriodoAcademicoModelo
    {
        [Key]
        [Column("pk_id_nombre_periodo_academico")]
        public int pkIdNombrePeriodoAcademico { get; set; } = 0;

        [Column("nombre_periodo_academico")]
        public string nombrePeriodoAcademico { get; set; } = string.Empty;

        [Column("estado_nombre_periodo_academico")]
        public string estadoNombrePeriodoAcademico { get; set; } = string.Empty;
    }
}

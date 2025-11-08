using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Asignatura
{
    [Table("asignatura")]
    public class AsignaturaModelo
    {
        [Key]
        [Column("pk_id_asignatura")]
        public int pkIdAsignatura { get; set; } = 0;

        [Column("nombre_asignatura")]
        public string nombreAsignatura { get; set; } = string.Empty;

        [Column("estado_asignatura")]
        public string estadoAsignatura { get; set; } = string.Empty;
    }
}

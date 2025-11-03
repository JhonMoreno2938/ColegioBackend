using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Jornada
{
    [Table("jornada")]
    public class JornadaModelo
    {
        [Key]
        [Column("pk_id_jornada")]
        public int pkIdJornada { get; set; } = 0;

        [Column("nombre_jornada")]
        public string nombreJornada { get; set; } = string.Empty;

        [Column("estado_jornada")]
        public string estadoJornada { get; set; } = string.Empty;
    }
}

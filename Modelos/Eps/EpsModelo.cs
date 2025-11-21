using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Eps
{
    [Table("eps")]
    public class EpsModelo
    {
        [Key]
        [Column("pk_id_EPS")]
        public int pkIdEps { get; set; } = 0;

        [Column("nombre_EPS")]
        public string nombreEps { get; set; } = string.Empty;

        [Column("estado_EPS")]
        public string estadoEps { get; set; } = string.Empty;
    }
}

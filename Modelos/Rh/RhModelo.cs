using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Rh
{
    [Table("rh")]
    public class RhModelo
    {
        [Key]
        [Column("pk_id_rh")]
        public int pkIdRh { get; set; } = 0;

        [Column("nombre_rh")]
        public string nombreRh { get; set; } = string.Empty;

        [Column("estado_rh")]
        public string estado_rh { get; set; } = string.Empty;
        public RhModelo()
        {

        }
    }
}

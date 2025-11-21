using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Discapcidad
{
    [Table("discapacidad")]
    public class DiscapacidadModelo
    {
        [Key]
        [Column("pk_id_discapacidad")]
        public int pkIdDiscapacidad = 0;

        [Column("nombre_discapacidad")]
        public string nombreDiscapacidad { get; set; } = string.Empty;

        [Column("estado_discapacidad")]
        public string estadoDiscapacidad { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Estrato_Social
{
    [Table("estrato_social")]
    public class EstratoSocialModelo
    {
        [Key]
        [Column("pk_id_estrato_social")]
        public int pkIdEstratoSocial { get; set; } = 0;

        [Column("nombre_estrato_social")]
        public string nombreEstratoSocial { get; set; } = string.Empty;

        [Column("estado_estrato_social")]
        public string estadoEstratoSocial { get; set; } = string.Empty;
    }
}

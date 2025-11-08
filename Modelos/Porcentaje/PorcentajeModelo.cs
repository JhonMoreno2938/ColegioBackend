using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Porcentaje
{
    [Table("porcentaje")]
    public class PorcentajeModelo
    {
        [Key]
        [Column("pk_id_porcentaje")]
        public int pkIdPorcentaje { get; set; } = 0;

        [Column("valor_porcentaje")]
        public int valorPorcentaje { get; set; } = 0;

        [Column("estado_porcentaje")]
        public string estadoPorcentaje { get; set; } = string.Empty;
    }
}

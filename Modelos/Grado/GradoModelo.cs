using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Grado
{
    [Table("grado")]
    public class GradoModelo
    {
        [Key]
        [Column("pk_id_grado")]
        public int pkIdGrado { get; set; } = 0;

        [Column("nombre_grado")]
        public string nombreGrado { get; set; } = string.Empty;

        [Column("estado_grado")]
        public string estadoGrado { get; set; } = string.Empty;
    }
}

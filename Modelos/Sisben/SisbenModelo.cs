using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Sisben
{
    [Table("sisben")]
    public class SisbenModelo
    {
        [Key]
        [Column("pk_id_sisben")]
        public int pkIdSisben { get; set; } = 0;

        [Column("nombre_sisben")]
        public string nombreSisben { get; set; } = string.Empty;

        [Column("estado_sisben")]
        public string estado_sisben { get; set; } = string.Empty;
    }
}

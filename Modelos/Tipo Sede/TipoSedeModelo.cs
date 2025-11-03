using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Tipo_Sede
{
    [Table("tipo_sede")]
    public class TipoSedeModelo
    {
        [Key]
        [Column("pk_id_tipo_sede")]
        public int pkIdTipoSede { get; set; } = 0;

        [Column("nombre_tipo_sede")]
        public string nombreTipoSede { get; set; } = string.Empty;

        [Column("estado_tipo_sede")]
        public string estado_tipo_sede { get; set; } = string.Empty;

        public TipoSedeModelo()
        {

        }

    }
}

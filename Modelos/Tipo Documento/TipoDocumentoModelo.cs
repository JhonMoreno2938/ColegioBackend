using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Tipo_Documento
{
    [Table("tipo_documento")]
    public class TipoDocumentoModelo
    {
        [Key]
        [Column("pk_id_tipo_documento")]
        public int pkIdTipoDocumento { get; set; } = 0;

        [Column("nombre_tipo_documento")]
        public string nombreTipoDocumento { get; set; } = string.Empty;

        [Column("estado_tipo_documento")]
        public string estadoTipoDocumento { get; set; } = string.Empty;
        public TipoDocumentoModelo()
        {

        }
    }
}

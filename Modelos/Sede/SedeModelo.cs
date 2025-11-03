using Colegio.Modelos.Tipo_Sede;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Sede
{
    [Table("sede")]
    public class SedeModelo
    {

        public TipoSedeModelo tipoSedeModelo = new TipoSedeModelo();

        [Key]
        [Column("pk_id_sede")]
        public int pkIdSede { get; set; } = 0;

        [Column("codigo_DANE_sede")]
        public string codigoDaneSede { get; set; } = string.Empty;

        [Column("nombre_sede")]
        public string nombreSede { get; set; } = string.Empty;

        [Column("direccion_sede")]
        public string direccionSede { get; set; } = string.Empty;

        [Column("numero_contacto_sede")]
        public string numeroContactoSede { get; set; } = string.Empty;

        [Column("estado_sede")]
        public string estadoSede { get; set; } = string.Empty;

        [Column("fk_id_tipo_sede")]
        public int fkIdTipoSede
        {
            get => tipoSedeModelo.pkIdTipoSede;
            set => tipoSedeModelo.pkIdTipoSede = value;
        }
        public SedeModelo()
        {

        }
    }
}

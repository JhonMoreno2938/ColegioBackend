using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Nivel_Escolaridad
{
    [Table("nivel_escolaridad")]
    public class NivelEscolaridadModelo
    {
        [Key]
        [Column("pk_id_nivel_escolaridad")]
        public int pkIdNivelEscolaridad { get; set; } = 0;

        [Column("nombre_nivel_escolaridad")]
        public string nombreNivelEscolaridad { get; set; } = string.Empty;

        [Column("estado_nivel_escolaridad")]
        public string estadoNivelEscolaridad { get; set; } = string.Empty;

        public NivelEscolaridadModelo()
        {

        }
    }
}

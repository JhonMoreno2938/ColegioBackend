using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Genero
{
    [Table("genero")]
    public class GeneroModelo
    {
        [Key]
        [Column("pk_id_genero")]
        public int pkIdGenero { get; set; } = 0;

        [Column("nombre_genero")]
        public string nombreGenero { get; set; } = string.Empty;

        [Column("estado_genero")]
        public string estadoGenero { get; set; } = string.Empty;
        public GeneroModelo()
        {

        }
    }
}

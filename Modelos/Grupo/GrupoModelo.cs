using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Grupo
{
    [Table("grupo")]
    public class GrupoModelo
    {
        [Key]
        [Column("pk_id_grupo")]
        public int pkIdGrupo { get; set; } = 0;

        [Column("nombre_grupo")]
        public string nombreGrupo { get; set; } = string.Empty;

        [Column("estado_grupo")]
        public string estadoGrupo { get; set; } = string.Empty;
    }
}

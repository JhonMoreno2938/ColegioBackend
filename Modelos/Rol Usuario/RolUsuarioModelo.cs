using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Rol_Usuario
{
    [Table("rol_usuario")]
    public class RolUsuarioModelo
    {
        [Key]
        [Column("pk_id_rol_usuario")]
        public int pkIdRolUsuario { get; set; } = 0;

        [Column("nombre_rol_usuario")]
        public string nombreRolUsuario { get; set; } = string.Empty;

        [Column("estado_rol_usuario")]
        public string estadoRolUsuario { get; set; } = string.Empty;
        public RolUsuarioModelo()
        {

        }
    }
}

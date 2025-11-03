using Colegio.Modelos.Rol_Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Usuario
{
    [Table("usuario")]
    public class UsuarioModelo
    {
        public RolUsuarioModelo rolUsuarioModelo = new RolUsuarioModelo();

        [Key]
        [Column("pk_id_usuario")]
        public int pkIdUsuario { get; set; } = 0;

        [Column("nombre_usuario")]
        public string nombreUsuario { get; set; } = string.Empty;

        [Column("contraseina_usuario")]
        public string contraseinaUsuario { get; set; } = string.Empty;

        [Column("estado_usuario")]
        public string estadoUsuario { get; set; } = string.Empty;

        [Column("fk_id_rol_usuario")]
        public int fkIdRolUsuario
        {
            get => rolUsuarioModelo.pkIdRolUsuario;
            set => rolUsuarioModelo.pkIdRolUsuario = 0;
        }
        public UsuarioModelo()
        {

        }
    }
}

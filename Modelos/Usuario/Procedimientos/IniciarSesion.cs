using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Usuario.Procedimientos
{
    public class IniciarSesion
    {
        [JsonIgnore]
        private readonly UsuarioModelo usuarioModelo = new UsuarioModelo();

        [JsonPropertyName("nombreUsuario")]
        [Required]
        [StringLength(10)]
        public string nombreUsuario
        {
            get => usuarioModelo.nombreUsuario;
            set => usuarioModelo.nombreUsuario = value;
        }

        [JsonPropertyName("contraseinaUsuario")]
        [Required]
        [StringLength(255)]
        public string contraseinaUsuario
        {
            get => usuarioModelo.contraseinaUsuario;
            set => usuarioModelo.contraseinaUsuario = value;
        }
    }
}

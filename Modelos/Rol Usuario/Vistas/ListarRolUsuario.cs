using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Rol_Usuario.Vistas
{
    public class ListarRolUsuario
    {
        [JsonIgnore]
        private readonly RolUsuarioModelo rolUsuarioModelo = new RolUsuarioModelo();

        [JsonPropertyName("nombreRolUsuario")]
        [StringLength(35)]
        public string nombreRolUsuario
        {
            get => rolUsuarioModelo.nombreRolUsuario;
            set => rolUsuarioModelo.nombreRolUsuario = value;
        }
    }
}

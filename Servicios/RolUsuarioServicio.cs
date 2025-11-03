using Colegio.Interfaz;
using Colegio.Modelos.Rol_Usuario.Vistas;

namespace Colegio.Servicios
{
    public class RolUsuarioServicio
    {
        private readonly IRolUsuario rolUsuario;

        public RolUsuarioServicio(IRolUsuario rolUsuario)
        {
            this.rolUsuario = rolUsuario;
        }
        public async Task<List<ListarRolUsuario>> ValidarInformacionRolUsuarioAsync()
        {
            var informacionRolUsuario = await rolUsuario.InformacionRolUsuarioAsync();

            return informacionRolUsuario;
        }
    }
}

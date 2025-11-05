using Colegio.Interfaz;
using Colegio.Modelos.Rol_Usuario.Vistas;
using Colegio.Modelos.Rol_Usuario;

namespace Colegio.Servicios
{
    public class RolUsuarioServicio
    {
        private readonly IRolUsuario rolUsuario;

        public RolUsuarioServicio(IRolUsuario rolUsuario)
        {
            this.rolUsuario = rolUsuario;
        }

        private List<ListarRolUsuario> MapearListarRolUsuario(List<RolUsuarioModelo> rolUsuarioModelo)
        {
            return rolUsuarioModelo.Select(modelo => new ListarRolUsuario
            {
                nombreRolUsuario = modelo.nombreRolUsuario
            }).ToList();
        }

        public async Task<List<ListarRolUsuario>> ValidarInformacionRolUsuarioAsync()
        {
            var modeloRolUsuario = await rolUsuario.InformacionRolUsuarioAsync();

            var resultado = MapearListarRolUsuario(modeloRolUsuario);

            return resultado;
        }
    }
}

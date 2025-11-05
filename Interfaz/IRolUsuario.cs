using Colegio.Modelos.Rol_Usuario;

namespace Colegio.Interfaz
{
    public interface IRolUsuario
    {
        Task<List<RolUsuarioModelo>> InformacionRolUsuarioAsync();
    }
}

using Colegio.Modelos.Rol_Usuario.Vistas;

namespace Colegio.Interfaz
{
    public interface IRolUsuario
    {
        Task<List<ListarRolUsuario>> InformacionRolUsuarioAsync();
    }
}

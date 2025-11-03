using Colegio.Modelos.Usuario.Procedimientos;
using Colegio.Modelos.Usuario.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface IUsuario
    {
        Task<SalidaIniciarSesion> IniciarSesionAsync(IniciarSesion iniciarSesion);
    }
}

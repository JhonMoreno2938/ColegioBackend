using Colegio.Interfaz;
using Colegio.Modelos.Usuario.Procedimientos;
using Colegio.Modelos.Usuario.Salidas_Procedimientos;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class UsuarioServicio
    {
        private readonly IUsuario usuario;

        public UsuarioServicio(IUsuario usuario)
        {
            this.usuario = usuario;
        }

        public async Task<SalidaIniciarSesion> ValidarIniciarSesionoAsync(IniciarSesion iniciarSesion)
        {
            SalidaIniciarSesion resultado = await usuario.IniciarSesionAsync(iniciarSesion);

            if (!resultado.exito)
            {
                return resultado;
            }

            string contraseinaCalculada = Seguridad.EncriptarContraseina(iniciarSesion.contraseinaUsuario, iniciarSesion.nombreUsuario);

            if (contraseinaCalculada == resultado.contraseinaUsuario)
            {
                return resultado;
            }
            else
            {
                resultado.exito = false;
                resultado.mensaje = "Contraseña incorrecta";

                resultado.nombreUsuario = string.Empty;
                resultado.nombreRolUsuario = string.Empty;


                return resultado;
            }
        }
    }
}
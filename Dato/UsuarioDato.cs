using Colegio.Interfaz;
using Colegio.Modelos.Usuario.Procedimientos;
using Colegio.Modelos.Usuario.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class UsuarioDato : IUsuario
    {
        private readonly string conexion;
        private static readonly string queryObtenerUsuario = "iniciar_sesion";

        public UsuarioDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<SalidaIniciarSesion> IniciarSesionAsync(IniciarSesion iniciarSesion)
        {
            var resultado = new SalidaIniciarSesion() { exito = false, mensaje = "Usuario no encontrado o error en la base de datos." };

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryObtenerUsuario, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_usuario", iniciarSesion.nombreUsuario);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    using var leer = await comando.ExecuteReaderAsync();

                    if (await leer.ReadAsync())
                    {
                        resultado.nombrePersona = leer["nombre_completo"]?.ToString() ?? string.Empty;
                        resultado.nombreUsuario = leer["nombre_usuario"]?.ToString() ?? string.Empty;
                        resultado.contraseinaUsuario = leer["contraseina_usuario"]?.ToString() ?? string.Empty;
                        resultado.nombreRolUsuario = leer["nombre_rol_usuario"]?.ToString() ?? string.Empty;

                        resultado.exito = true;

                        if (outMensaje.Value != null && outMensaje.Value != DBNull.Value)
                        {
                            resultado.mensaje = outMensaje.Value.ToString() ?? string.Empty;
                        }
                    }
                    else
                    {
                        await leer.CloseAsync();

                        if (outMensaje.Value != null && outMensaje.Value != DBNull.Value)
                        {
                            resultado.mensaje = outMensaje.Value.ToString() ?? "Usuario no encontrado.";
                        }
                        else
                        {
                            resultado.mensaje = "Usuario no encontrado.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error al ejecutar el procedimiento {queryObtenerUsuario}: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                resultado.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultado;
        }
    }
}
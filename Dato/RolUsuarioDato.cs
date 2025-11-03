using Colegio.Interfaz;
using Colegio.Modelos.Rol_Usuario.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class RolUsuarioDato : IRolUsuario
    {
        private readonly string conexion;
        private static readonly string queryListaRolUsuario = "select nombre_rol_usuario from listar_roles_usuarios";

        public RolUsuarioDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<List<ListarRolUsuario>> InformacionRolUsuarioAsync()
        {
            var listaRolUsuario = new List<ListarRolUsuario>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaRolUsuario, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarRolUsuario = new ListarRolUsuario();
                                listarRolUsuario.nombreRolUsuario = leer.GetString("nombre_rol_usuario");
                                listaRolUsuario.Add(listarRolUsuario);
                            }
                        }
                    }
                }
                return listaRolUsuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los roles de usuario: {ex.Message}");
                return new List<ListarRolUsuario>();
            }
        }
    }
}

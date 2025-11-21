using Colegio.Interfaz;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class AuditoriaDato : IAuditoria
    {
        private readonly string conexion;
        private static readonly string queryAuditoriaCsv = "auditoria_csv";

        public AuditoriaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<bool> AuditoriaCargueCsv(string nombreUsuario)
        {
            bool resultado = false;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryAuditoriaCsv, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_usuario", nombreUsuario);

                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error al ejecutar el procedimiento de auditoría: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }
    }
}

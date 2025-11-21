using Colegio.Interfaz;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class FuncionarioAsignaturaDato : IFuncionarioAsignatura
    {
        private readonly string conexion;
        private static readonly string queryAsignarFuncionarioAsignatura = "asignar_funcionario_asignatura";
        private static readonly string queryGestionarEstadoFuncionarioAsignatura = "gestionar_estado_funcionario_asignatura";


        public FuncionarioAsignaturaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<bool> AsignarFuncionarioAsignaturaAsync(string numeroDocumento, string nombreAsignatura, string listaGrado, string listaGrupo, string listaSede, string listaJornada)
        {
            bool resultado = false;

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryAsignarFuncionarioAsignatura, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);
                    comando.Parameters.AddWithValue("@lista_grado", listaGrado);
                    comando.Parameters.AddWithValue("@lista_grupo", listaGrupo);
                    comando.Parameters.AddWithValue("@lista_sede", listaSede);
                    comando.Parameters.AddWithValue("@lista_jornada", listaJornada);


                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }

                    if (resultado)
                    {
                        await transaccion.CommitAsync();
                    }
                    else
                    {
                        await transaccion.RollbackAsync();
                    }
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        await transaccion.RollbackAsync();
                    }
                    throw new ApplicationException($"Error al ejecutar el procedimiento: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        public async Task<ResultadoOperacion> GestionarEstadoFuncionarioAsignaturaAsync(string operacion, int idFuncionarioAsignaturaGradoGrupo)
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoFuncionarioAsignatura, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("p_id_funcionario_asignatura_grado_grupo", idFuncionarioAsignaturaGradoGrupo);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP != null)
                    {
                        resultado.exitoso = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exitoso)
                    {
                        await transaccion.CommitAsync();
                    }
                    else
                    {
                        await transaccion.RollbackAsync();
                    }
                }
                catch (MySqlException ex)
                {
                    await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error inesperado: {ex.Message}";
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

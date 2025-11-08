using Colegio.Interfaz;
using Colegio.Modelos.Asignatura_Grado_Grupo.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class AsignaturaGradoGrupoDato : IAsignaturaGradoGrupo
    {
        private readonly string conexion;
        private static readonly string queryRegistrarAsignaturaGradoGrupo = "registrar_asignatura_grado_grupo";
        private static readonly string queryGestionarEstadoAsignaturaGradoGrupo = "gestionar_estado_asignatura_grado_grupo";


        public AsignaturaGradoGrupoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeAsignaturaGradoGrupo> RegistrarAsignaturaGradoGrupoAsync(string nombreAsignatura, string listaGrado, string listaGrupo, string listaNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeAsignaturaGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarAsignaturaGradoGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);
                    comando.Parameters.AddWithValue("@lista_grado", listaGrado);
                    comando.Parameters.AddWithValue("@lista_grupo", listaGrupo);
                    comando.Parameters.AddWithValue("@lista_nivel_escolaridad", listaNivelEscolaridad);


                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se le asignaron"))
                    {
                        resultado.exito = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exito)
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

        public async Task<ResultadoMensajeAsignaturaGradoGrupo> GestionarEstadoAsignaturaGradoGrupoAsync(string operacion, string nombreGrado, string nombreGrupo, string nombreNivelEscolaridad, string nombreAsignatura)
        {
            var resultado = new ResultadoMensajeAsignaturaGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoAsignaturaGradoGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_nombre_grado", nombreGrado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", nombreGrupo);
                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", nombreNivelEscolaridad);
                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("activo") || mensajeSP.Contains("inactivo"))
                    {
                        resultado.exito = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exito)
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
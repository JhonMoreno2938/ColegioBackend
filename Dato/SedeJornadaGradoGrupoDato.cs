using Colegio.Interfaz;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Sede_Jornada_Grado_Grupo.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class SedeJornadaGradoGrupoDato : ISedeJornadaGradoGrupo
    {
        private readonly string conexion;
        private static readonly string queryRegistrarGradoGrupoNivelEscolaridadSede = "registrar_grado_grupo_nivel_escolaridad_sede";
        private static readonly string queryGestionarSedeJornadaGradoGrupo = "gestionar_estado_grado_grupo_sede";


        public SedeJornadaGradoGrupoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeSedeJornadaGradoGrupo> RegistrarGradoGrupoNivelEscolaridadAsync(string codigoDane, string nombreJornada, string listaGrado, string listaGrupo)
        {
            var resultado = new ResultadoMensajeSedeJornadaGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarGradoGrupoNivelEscolaridadSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDane);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", nombreJornada);
                    comando.Parameters.AddWithValue("@lista_grado", listaGrado);
                    comando.Parameters.AddWithValue("@lista_grupo", listaGrupo);

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

        public async Task<ResultadoMensajeSedeJornadaGradoGrupo> GestionarEstadoGradoGrupoSedeAsync(string operacion, string nombreGrado, string nombreGrupo, string nombreNivelEscolaridad, string codigoDaneSede, string nombreJornada)
        {
            var resultado = new ResultadoMensajeSedeJornadaGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarSedeJornadaGradoGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_nombre_grado", nombreGrado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", nombreGrupo);
                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", nombreNivelEscolaridad);
                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDaneSede);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", nombreJornada);

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

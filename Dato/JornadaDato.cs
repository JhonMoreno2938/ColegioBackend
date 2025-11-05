using Colegio.Interfaz;
using Colegio.Modelos.Jornada;
using Colegio.Modelos.Jornada.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class JornadaDato : IJornada
    {
        private readonly string conexion;
        private static readonly string queryRegistrarJornada = "registrar_jornada";
        private static readonly string queryGestionarEstadoJornada = "gestionar_estado_jornada";
        private static readonly string queryListaJornada = "select nombre_jornada, estado_jornada from listar_jornadas";
        private static readonly string queryListaJornadaActivo = "select nombre_jornada from listar_jornadas_estado_activo";

        public JornadaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }


        public async Task<ResultadoMensajeJornada> RegistrarJornadaAsync(JornadaModelo jornadaModelo)
        {
            var resultado = new ResultadoMensajeJornada { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarJornada, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_jornada", jornadaModelo.nombreJornada);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se registró"))
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

        public async Task<ResultadoMensajeJornada> GestionarEstadoJornadaAsync(string operacion, JornadaModelo jornadaModelo)
        {
            var resultado = new ResultadoMensajeJornada { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoJornada, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", jornadaModelo.nombreJornada);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se activo") || mensajeSP.StartsWith("Se inactivo"))
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

        public async Task<List<JornadaModelo>> InformacionJornadaAsync()
        {
            var listaJornada = new List<JornadaModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaJornada, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarJornada = new JornadaModelo();
                                listarJornada.nombreJornada = leer.GetString("nombre_jornada");
                                listarJornada.estadoJornada = leer.GetString("estado_jornada");
                                listaJornada.Add(listarJornada);
                            }
                        }
                    }
                }
                return listaJornada;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las jornadas: {ex.Message}");
                return new List<JornadaModelo>();
            }
        }

        public async Task<List<JornadaModelo>> InformacionJornadaEstadoActivoAsync()
        {
            var listaJornadaEstadoActivo = new List<JornadaModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaJornadaActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarJornadaEstadoActivo = new JornadaModelo();
                                listarJornadaEstadoActivo.nombreJornada = leer.GetString("nombre_jornada");
                                listaJornadaEstadoActivo.Add(listarJornadaEstadoActivo);
                            }
                        }
                    }
                }
                return listaJornadaEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las jornadas: {ex.Message}");
                return new List<JornadaModelo>();
            }
        }

    }
}

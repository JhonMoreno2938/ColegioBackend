using Colegio.Interfaz;
using Colegio.Modelos.Jornada_Sede;
using Colegio.Modelos.Jornada_Sede.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class JornadaSedeDato : IJornadaSede
    {
        private readonly string conexion;
        private static readonly string queryRegistrarJornadaSede = "registrar_jornada_sede";
        private static readonly string queryGestionarEstadoJornadaSede = "gestionar_estado_jornada_sede";
        private static readonly string queryMostrarJornadaAsociadaSede = "mostrar_jornadas_asociadas_sede";
        private static readonly string queryMostrarJornadaAsociadaSedeActivo = "mostrar_jornadas_asociadas_sede_estado_activo";

        public JornadaSedeDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeJornadaSede> RegistrarJornadaSedeAsync(string codigoDane, string listaJornada)
        {
            var resultado = new ResultadoMensajeJornadaSede { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarJornadaSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDane);
                    comando.Parameters.AddWithValue("@lista_jornada", listaJornada);

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

        public async Task<ResultadoMensajeJornadaSede> GestionarEstadoJornadaSedeAsync(string operacion, string codigoDane, string nombreJornada)
        {
            var resultado = new ResultadoMensajeJornadaSede { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoJornadaSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDane);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", nombreJornada);

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

        public async Task<List<JornadaSedeModelo>> InformacionJornadaAsociadaSede(string codigoDaneSede)
        {
            var listaJornada = new List<JornadaSedeModelo>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryMostrarJornadaAsociadaSede, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDaneSede);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {
                        var modelo = new JornadaSedeModelo();

                        modelo.jornadaModelo.nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty;
                        modelo.estadoJornadaSede = leer["estado_jornada_sede"]?.ToString() ?? string.Empty;

                        listaJornada.Add(modelo);
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error inesperado al procesar los resultados: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión a la base de datos: {ex.Message}");
                return listaJornada;
            }

            return listaJornada;
        }

        public async Task<List<JornadaSedeModelo>> InformacionJornadaAsociadaSedeEstadoActivo(string codigoDaneSede)
        {

            var listaJornada = new List<JornadaSedeModelo>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryMostrarJornadaAsociadaSedeActivo, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDaneSede);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {
                        var modelo = new JornadaSedeModelo();

                        modelo.jornadaModelo.nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty;

                        listaJornada.Add(modelo);
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error inesperado al procesar los resultados: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión a la base de datos: {ex.Message}");
                return listaJornada;
            }

            return listaJornada;
        }
    }
}
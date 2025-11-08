using Colegio.Interfaz;
using Colegio.Modelos.Porcentaje;
using Colegio.Modelos.Porcentaje.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class PorcentajeDato : IPorcentaje
    {
        private readonly string conexion;
        private static readonly string queryRegistrarPorcentaje = "registrar_porcentaje";
        private static readonly string queryGestionarEstadoPorcentaje = "gestionar_estado_porcentaje";
        private static readonly string queryListaPorcentaje = "select valor_porcentaje, estado_porcentaje from listar_porcentajes";
        private static readonly string queryListaPorcentajeActivo = "select valor_porcentaje from listar_porcentajes_estado_activo";


        public PorcentajeDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajePorcentaje> RegistrarPorcentajeAsync(PorcentajeModelo porcentajeModelo)
        {
            var resultado = new ResultadoMensajePorcentaje { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarPorcentaje, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_valor_porcentaje", porcentajeModelo.valorPorcentaje);

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

        public async Task<ResultadoMensajePorcentaje> GestionarEstadoPorcentajeAsync(string operacion, PorcentajeModelo porcentajeModelo)
        {
            var resultado = new ResultadoMensajePorcentaje { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoPorcentaje, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_valor_porcentaje", porcentajeModelo.valorPorcentaje);

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

        public async Task<List<PorcentajeModelo>> InformacionPorcentajeAsync()
        {

            var listaPorcentaje = new List<PorcentajeModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaPorcentaje, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarPorcentaje = new PorcentajeModelo();
                                listarPorcentaje.valorPorcentaje = leer["valor_porcentaje"] != DBNull.Value ? Convert.ToInt32(leer["valor_porcentaje"]) : 0;
                                listarPorcentaje.estadoPorcentaje = leer.GetString("estado_porcentaje");
                                listaPorcentaje.Add(listarPorcentaje);
                            }
                        }
                    }
                }
                return listaPorcentaje;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los porcentajes: {ex.Message}");
                return new List<PorcentajeModelo>();
            }
        }

        public async Task<List<PorcentajeModelo>> InformacionPorcentajeEstadoActivoAsync()
        {

            var listaPorcentajeEstadoActivo = new List<PorcentajeModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaPorcentajeActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarPorcentajeEstadoActivo = new PorcentajeModelo();
                                listarPorcentajeEstadoActivo.valorPorcentaje = leer["valor_porcentaje"] != DBNull.Value ? Convert.ToInt32(leer["valor_porcentaje"]) : 0;
                                listaPorcentajeEstadoActivo.Add(listarPorcentajeEstadoActivo);
                            }
                        }
                    }
                }
                return listaPorcentajeEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los porcentajes: {ex.Message}");
                return new List<PorcentajeModelo>();
            }
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Grado.Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Modelos.Grado.Salidas_Procedimientos;
using Colegio.Modelos.Grado.Vistas;

namespace Colegio.Dato
{
    public class GradoDato : IGrado
    {
        private readonly string conexion;
        private static readonly string queryRegistrarGrado = "registrar_grado";
        private static readonly string queryGestionarEstadoGrado = "gestionar_estado_grado";
        private static readonly string queryListaGrado = "select nombre_grado, estado_grado from listar_grados";
        private static readonly string queryListaGradoActivo = "select nombre_grado from listar_grados_estado_activo";

        public GradoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<ResultadoMensajeGrado> RegistrarGradoAsync(RegistrarGrado registrarGrado)
        {
            var resultado = new ResultadoMensajeGrado { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarGrado, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_grado", registrarGrado.nombreGrado);

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

        public async Task<ResultadoMensajeGrado> GestionarEstadoGradoAsync(GestionarEstadoGrado gestionarEstadoGrado)
        {
            var resultado = new ResultadoMensajeGrado { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoGrado, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", gestionarEstadoGrado.nombreOperacion);
                    comando.Parameters.AddWithValue("@p_nombre_grado", gestionarEstadoGrado.nombreGrado);

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

        public async Task<List<ListarGrado>> InformacionGradoAsync()
        {
            var listaGrado = new List<ListarGrado>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGrado, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGrado = new ListarGrado();
                                listarGrado.nombreGrado = leer.GetString("nombre_grado");
                                listarGrado.estadoGrado = leer.GetString("estado_grado");
                                listaGrado.Add(listarGrado);
                            }
                        }
                    }
                }
                return listaGrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grados: {ex.Message}");
                return new List<ListarGrado>();
            }
        }

        public async Task<List<ListarGradoEstadoActivo>> InformacionGradoEstadoActivoAsync()
        {
            var listaGradoEstadoActivo = new List<ListarGradoEstadoActivo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGradoActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGradoEstadoActivo = new ListarGradoEstadoActivo();
                                listarGradoEstadoActivo.nombreGrado = leer.GetString("nombre_grado");
                                listaGradoEstadoActivo.Add(listarGradoEstadoActivo);
                            }
                        }
                    }
                }
                return listaGradoEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grados: {ex.Message}");
                return new List<ListarGradoEstadoActivo>();
            }
        }
    }
}

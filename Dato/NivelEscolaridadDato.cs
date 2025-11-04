using Colegio.Interfaz;
using Colegio.Modelos.Nivel_Escolaridad.Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Salidas_Procedimientos;
using Colegio.Modelos.Nivel_Escolaridad.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class NivelEscolaridadDato : INivelEscolaridad
    {
        private readonly string conexion;
        private static readonly string queryRegistrarNivelEscolaridad = "registrar_nivel_escolaridad";
        private static readonly string queryGestionarEstadoNivelEscolaridad = "gestionar_estado_nivel_escolaridad";
        private static readonly string queryListaNivelEscolaridad = "select nombre_nivel_escolaridad, estado_nivel_escolaridad from listar_nivel_escolaridad";
        private static readonly string queryListaNivelEscolaridadActivo = "select nombre_nivel_escolaridad from listar_nivel_escolaridad_estado_activo";

        public NivelEscolaridadDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeNivelEscolaridad> RegistrarNivelEscolaridadAsync(RegistrarNivelEscolaridad registrarNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeNivelEscolaridad { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarNivelEscolaridad, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", registrarNivelEscolaridad.nombreNivelEscolaridad);

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

        public async Task<ResultadoMensajeNivelEscolaridad> GestionarEstadoNivelEscolaridadAsync(GestionarEstadoNivelEscolaridad gestionarEstadoNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeNivelEscolaridad { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoNivelEscolaridad, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", gestionarEstadoNivelEscolaridad.nombreOperacion);
                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", gestionarEstadoNivelEscolaridad.nombreNivelEscolaridad);

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

        public async Task<List<ListarNivelEscolaridad>> InformacionNivelEscolaridadAsync()
        {
            var listaNivelEscolaridad = new List<ListarNivelEscolaridad>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaNivelEscolaridad, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarNivelEscolaridad = new ListarNivelEscolaridad();
                                listarNivelEscolaridad.nombreNivelEscolaridad = leer.GetString("nombre_nivel_escolaridad");
                                listarNivelEscolaridad.estadoNivelEscolaridad = leer.GetString("estado_nivel_escolaridad");
                                listaNivelEscolaridad.Add(listarNivelEscolaridad);
                            }
                        }
                    }
                }
                return listaNivelEscolaridad;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los niveles de escolaridad: {ex.Message}");
                return new List<ListarNivelEscolaridad>();
            }
        }

        public async Task<List<ListarNivelEscolaridadEstadoActivo>> InformacionNivelEscolaridadEstadoActivoAsync()
        {
            var listaNivelEscolaridadEstadoActivo = new List<ListarNivelEscolaridadEstadoActivo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaNivelEscolaridadActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarNivelEscolaridadEstadoActivo = new ListarNivelEscolaridadEstadoActivo();
                                listarNivelEscolaridadEstadoActivo.nombreNivelEscolaridad = leer.GetString("nombre_nivel_escolaridad");
                                listaNivelEscolaridadEstadoActivo.Add(listarNivelEscolaridadEstadoActivo);
                            }
                        }
                    }
                }
                return listaNivelEscolaridadEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los niveles de escolaridad: {ex.Message}");
                return new List<ListarNivelEscolaridadEstadoActivo>();
            }
        }
    }
}

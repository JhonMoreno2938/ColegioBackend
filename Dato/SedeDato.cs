using Colegio.Interfaz;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Modelos.Sede.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Sede;

namespace Colegio.Dato
{
    public class SedeDato : ISede
    {
        private readonly string conexion;
        private static readonly string queryRegistrarSede = "registrar_sede";
        private static readonly string queryModificarInformacionSede = "modificar_informacion_sede";
        private static readonly string queryGestionarEstadoSede = "gestionar_estado_sede";
        private static readonly string queryConsultarSede = "consultar_sede";
        private static readonly string queryListaSede = "select codigo_DANE_sede, nombre_sede, estado_sede from listar_sedes";
        private static readonly string queryListaSedeActivo = "select codigo_DANE_sede, nombre_sede from listar_sedes_estado_activo";

        public SedeDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeSede> RegistrarSedeAsync(SedeModelo sedeModelo)
        {
            var resultado = new ResultadoMensajeSede { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", sedeModelo.codigoDaneSede);
                    comando.Parameters.AddWithValue("@p_nombre_sede", sedeModelo.nombreSede);
                    comando.Parameters.AddWithValue("@p_direccion_sede", sedeModelo.direccionSede);
                    comando.Parameters.AddWithValue("@p_numero_contacto_sede", sedeModelo.numeroContactoSede);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_sede", sedeModelo.tipoSedeModelo.nombreTipoSede);

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

        public async Task<ResultadoMensajeSede> ModificarInformacionSedeAsync(SedeModelo sedeModelo)
        {
            var resultado = new ResultadoMensajeSede { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryModificarInformacionSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE_sede", sedeModelo.codigoDaneSede);
                    comando.Parameters.AddWithValue("@p_nuevo_nombre_sede", sedeModelo.nombreSede);
                    comando.Parameters.AddWithValue("@p_nueva_direccion_sede", sedeModelo.direccionSede);
                    comando.Parameters.AddWithValue("@p_nuevo_numero_contacto_sede", sedeModelo.numeroContactoSede);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se modificó"))
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

        public async Task<ResultadoMensajeSede> GestionarEstadoSedeAsync(string operacion, SedeModelo sedeModelo)
        {
            var resultado = new ResultadoMensajeSede { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoSede, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", operacion);
                    comando.Parameters.AddWithValue("@p_codigo_DANE_sede", sedeModelo.codigoDaneSede);

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
        public async Task<SalidaConsultarSede> ConsultarSedeAsync(string codigoDaneSede)
        {
            var resultado = new SalidaConsultarSede() { exito = false };

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarSede, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_codigo_DANE", codigoDaneSede);

                    using var leer = await comando.ExecuteReaderAsync();

                    // Proceda el primer select de la consulta.

                    if (await leer.ReadAsync())
                    {
                        resultado.codigoDaneSede = leer["codigo_DANE_sede"]?.ToString() ?? string.Empty;
                        resultado.nombreSede = leer["nombre_sede"]?.ToString() ?? string.Empty;
                        resultado.direccionSede = leer["direccion_sede"]?.ToString() ?? string.Empty;
                        resultado.numeroContactoSede = leer["numero_contacto_sede"]?.ToString() ?? string.Empty;
                        resultado.estadoSede = leer["estado_sede"]?.ToString() ?? string.Empty;
                        resultado.nombreTipoSede = leer["nombre_tipo_sede"]?.ToString() ?? string.Empty;

                        resultado.exito = true;
                    }
                    else
                    {
                        resultado.mensaje = "Sede no encontrada o código DANE incorrecto.";
                        return resultado;
                    }

                    // Procesa el segundo select de la consulta.

                    if (await leer.NextResultAsync())
                    {
                        while (await leer.ReadAsync())
                        {
                            resultado.GradosGruposVinculados.Add(new GradoGrupoDetalle
                            {
                                nombreGradoGrupo = leer["nombre_grado_grupo"]?.ToString() ?? string.Empty,
                                nombreNivelEscolaridad = leer["nombre_nivel_escolaridad"]?.ToString() ?? string.Empty,
                                nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty,
                                estadoSedeJornadaGradoGrupo = leer["estado_sede_jornada_grado_grupo"]?.ToString() ?? string.Empty
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error inesperado al procesar los resultados: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                resultado.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultado;
        }

        public async Task<List<SedeModelo>> InformacionSedeAsync()
        {
            var listaSede = new List<SedeModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaSede, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarSede = new SedeModelo();
                                listarSede.codigoDaneSede = leer.GetString("codigo_DANE_sede");
                                listarSede.nombreSede = leer.GetString("nombre_sede");
                                listarSede.estadoSede = leer.GetString("estado_sede");
                                listaSede.Add(listarSede);
                            }
                        }
                    }
                }
                return listaSede;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las sedes: {ex.Message}");
                return new List<SedeModelo>();
            }

        }

        public async Task<List<SedeModelo>> InformacionSedeEstadoActivoAsync()
        {
            var listaSedeEstadoActivo = new List<SedeModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaSedeActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarSedeEstadoActivo = new SedeModelo();
                                listarSedeEstadoActivo.codigoDaneSede = leer.GetString("codigo_DANE_sede");
                                listarSedeEstadoActivo.nombreSede = leer.GetString("nombre_sede");
                                listaSedeEstadoActivo.Add(listarSedeEstadoActivo);
                            }
                        }
                    }
                }
                return listaSedeEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las sedes: {ex.Message}");
                return new List<SedeModelo>();
            }
        }
    }
}

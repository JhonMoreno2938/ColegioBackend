using Colegio.Interfaz;
using Colegio.Modelos.Periodo_Academico.Salidas_Procedimientos;
using Colegio.Modelos.Periodo_Academico.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class PeriodoAcademicoDato : IPeriodoAcademico
    {
        private readonly string conexion;
        private static readonly string queryRegistrarPeriodoAcademico = "registrar_periodo_academico";
        private static readonly string queryConsultarPeriodoAcademico = "consultar_periodo_academico";
        private static readonly string queryModificarPeriodoAcademico = "modificar_periodo_academico";
        private static readonly string queryListaNombrePeriodoAcademico = "select pk_id_periodo_academico, nombre_periodo_academico from listar_nombre_periodos_academicos";
        private static readonly string queryListaAnnioPeriodoAcademico = "select annio_periodo_academico from listar_annio_periodos_academicos";
        private static readonly string queryObtenerPeriodoAcademicoAnnio = "obtener_periodo_academico_annio";

        public PeriodoAcademicoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<ResultadoMensajePeriodoAcademico> RegistrarPeriodoAcademicoAsync(string nombrePeriodoAcademico, int valorPorcentaje, string fechaInicioPeriodoAcademico, string fechaFinalPeriodoAcademico)
        {
            var resultado = new ResultadoMensajePeriodoAcademico { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarPeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_periodo_academico", nombrePeriodoAcademico);
                    comando.Parameters.AddWithValue("@p_valor_porcentaje", valorPorcentaje);
                    comando.Parameters.AddWithValue("@p_fecha_inicio_periodo_academico", fechaInicioPeriodoAcademico);
                    comando.Parameters.AddWithValue("@p_fecha_final_periodo_academico", fechaFinalPeriodoAcademico);

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
        public async Task<SalidaConsultarPeriodoAcademico> ConsultarPeriodoAcademicoAsync(int idPeriodoAcademico)
        {
            SalidaConsultarPeriodoAcademico resultado = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarPeriodoAcademico, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_id_periodo_academico", idPeriodoAcademico);

                    using var leer = await comando.ExecuteReaderAsync();

                    if (await leer.ReadAsync())
                    {
                        resultado = new SalidaConsultarPeriodoAcademico();

                        resultado.pkIdPeriodoAcademico = leer["pk_id_periodo_academico"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_periodo_academico"]) : 0;
                        string fechaInicioCompleta = leer["fecha_inicio"]?.ToString() ?? string.Empty;
                        string fechaFinalCompleta = leer["fecha_final"]?.ToString() ?? string.Empty;
                        resultado.fechaInicioPeriodoAcademico = fechaInicioCompleta.Length >= 10 ? fechaInicioCompleta.Substring(0, 10) : fechaInicioCompleta;
                        resultado.fechaFinalPeriodoAcademico = fechaFinalCompleta.Length >= 10 ? fechaFinalCompleta.Substring(0, 10) : fechaFinalCompleta;
                        resultado.estadoPeriodoAcademico = leer["estado_periodo_academico"]?.ToString() ?? string.Empty;
                        resultado.nombrePeriodoAcademico = leer["nombre_periodo_academico"]?.ToString() ?? string.Empty;
                        resultado.valorPorcentaje = leer["valor_porcentaje"] != DBNull.Value ? Convert.ToInt32(leer["valor_porcentaje"]) : 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error inesperado al procesar los resultados: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error de conexión a la base de datos: {ex.Message}", ex);
            }

            return resultado; // Retorna el DTO con datos o null.
        }

        public async Task<ResultadoMensajePeriodoAcademico> ModificarPeriodoAcademico(int idPeriodoAcademico, string fechaInicioPeriodoAcademico, string fechaFinalPeriodoAcademico, int valorPorcentaje)
        {

            var resultado = new ResultadoMensajePeriodoAcademico { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryModificarPeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_id_periodo_academico", idPeriodoAcademico);
                    comando.Parameters.AddWithValue("@p_fecha_inicio_periodo_academico", fechaInicioPeriodoAcademico);
                    comando.Parameters.AddWithValue("@p_fecha_final_periodo_academico", fechaFinalPeriodoAcademico);
                    comando.Parameters.AddWithValue("@p_valor_porcentaje", valorPorcentaje);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se modifico"))
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

        public async Task<List<ListarNombrePeriodoAcademico>> InformacionNombrePeriodoAcademico()
        {
            var listaNombrePeriodoAcademico = new List<ListarNombrePeriodoAcademico>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaNombrePeriodoAcademico, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarNombrePeriodoAcademico = new ListarNombrePeriodoAcademico();
                                listarNombrePeriodoAcademico.idPeriodoAcademico = leer["pk_id_periodo_academico"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_periodo_academico"]) : 0;
                                listarNombrePeriodoAcademico.nombrePeriodoAcademico = leer.GetString("nombre_periodo_academico");
                                listaNombrePeriodoAcademico.Add(listarNombrePeriodoAcademico);
                            }
                        }
                    }
                }
                return listaNombrePeriodoAcademico;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los nombre de los periodos academicos: {ex.Message}");
                return new List<ListarNombrePeriodoAcademico>();
            }
        }

        public async Task<List<ListarAnnioPeriodoAcademico>> InformacionAnnioPeriodoAcademico()
        {
            var listaAnnioPeriodoAcademico = new List<ListarAnnioPeriodoAcademico>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaAnnioPeriodoAcademico, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarAnnioPeriodoAcademico = new ListarAnnioPeriodoAcademico();
                                listarAnnioPeriodoAcademico.annioPeriodoAcademico = leer["annio_periodo_academico"] != DBNull.Value ? Convert.ToInt32(leer["annio_periodo_academico"]) : 0;
                                listaAnnioPeriodoAcademico.Add(listarAnnioPeriodoAcademico);
                            }
                        }
                    }
                }
                return listaAnnioPeriodoAcademico;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los nombre de los periodos academicos: {ex.Message}");
                return new List<ListarAnnioPeriodoAcademico>();
            }

        }

        public async Task<List<SalidaObtenerPeriodoAcademicoAnnio>> ObtenerPeriodoAcademicoAsync(int annio)
        {
            var listaResultados = new List<SalidaObtenerPeriodoAcademicoAnnio>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryObtenerPeriodoAcademicoAnnio, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_annio", annio);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {

                        var resultado = new SalidaObtenerPeriodoAcademicoAnnio();

                        resultado.idPeriodoAcademico = leer["pk_id_periodo_academico"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_periodo_academico"]) : 0;
                        string fechaInicioCompleta = leer["fecha_inicio"]?.ToString() ?? string.Empty;
                        string fechaFinalCompleta = leer["fecha_final"]?.ToString() ?? string.Empty;
                        resultado.fechaInicioPeriodoAcademico = fechaInicioCompleta.Length >= 10 ? fechaInicioCompleta.Substring(0, 10) : fechaInicioCompleta;
                        resultado.fechaFinalPeriodoAcademico = fechaFinalCompleta.Length >= 10 ? fechaFinalCompleta.Substring(0, 10) : fechaFinalCompleta;
                        resultado.estadoPeriodoAcademico = leer["estado_periodo_academico"]?.ToString() ?? string.Empty;
                        resultado.nombrePeriodoAcademico = leer["nombre_periodo_academico"]?.ToString() ?? string.Empty;
                        resultado.valorPorcentaje = leer["valor_porcentaje"]?.ToString() ?? string.Empty;

                        listaResultados.Add(resultado);
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error inesperado al procesar los resultados: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error de conexión a la base de datos: {ex.Message}", ex);
            }

            return listaResultados;
        }
    }
}

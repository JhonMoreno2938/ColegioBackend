using Colegio.Interfaz;
using Colegio.Modelos.Competencia;
using Colegio.Modelos.Competencia.Procedimientos;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class CompetenciaDato : ICompetencia
    {
        private readonly string conexion;
        private static readonly string queryRegistrarCompetencia = "registrar_competencia";
        private static readonly string queryListarCompetenciaAsociadaGradoGrupo = "consultar_competencia_asociada_grado_grupo";
        private static readonly string queryModificarDescripcionCompetencia = "modificar_descripcion_competencia";
        private static readonly string queryEliminarCompetencia = "eliminar_competencia";


        public CompetenciaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<ResultadoOperacion> RegistrarCompetenciaAsync(CompetenciaModelo competenciaModelo)
        {
            var resultadoOperacion = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarCompetencia, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_descripcion_competencia", competenciaModelo.descripcionCompetencia);
                    comando.Parameters.AddWithValue("@p_id_funcionario_asignatura_grado_grupo", competenciaModelo.funcionarioAsignaturaGradoGrupoModelo.pkIdFuncionarioAsignaturaGradoGrupo);
                    comando.Parameters.AddWithValue("@p_id_periodo_academico", competenciaModelo.periodoAcademicoModelo.pkIdPeriodoAcademico);
                    

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Esa competencia") || mensajeSP.StartsWith("Se registró"))
                    {
                        resultadoOperacion.exitoso = true;
                    }

                    resultadoOperacion.mensaje = mensajeSP;

                    if (resultadoOperacion.exitoso)
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
                    resultadoOperacion.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultadoOperacion.mensaje = $"Error inesperado: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultadoOperacion;
        }

        public async Task<List<ConsultarCompetenciaAsociadaGradoGrupoSalida>> InformacionCompetenciaAsociadaGradoGrupoAsync(CompetenciaModelo competenciaModelo)
        {
            var listarCompetencia = new List<ConsultarCompetenciaAsociadaGradoGrupoSalida>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryListarCompetenciaAsociadaGradoGrupo, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_id_funcionario_asignatura_grado_grupo", competenciaModelo.funcionarioAsignaturaGradoGrupoModelo.pkIdFuncionarioAsignaturaGradoGrupo);
                    comando.Parameters.AddWithValue("@p_id_periodo_academico", competenciaModelo.periodoAcademicoModelo.pkIdPeriodoAcademico);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {
                        var listarCompetencias = new ConsultarCompetenciaAsociadaGradoGrupoSalida
                        {
                            
                            idCompetencia = leer["pk_id_competencia"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_competencia"]) : 0,
                            descripcionCompetencia = leer["descripcion_competencia"]?.ToString() ?? string.Empty,
                        };

                        listarCompetencia.Add(listarCompetencias);

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

            return listarCompetencia; // Retorna el DTO con datos o null.
        }

        public async Task<ResultadoOperacion> ModificarDescripcionCompetenciaAsync(CompetenciaModelo competenciaModelo)
        {
            var resultadoOperacion = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryModificarDescripcionCompetencia, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_id_competencia", competenciaModelo.pkIdCompetencia);
                    comando.Parameters.AddWithValue("@p_descripcion_competencia", competenciaModelo.descripcionCompetencia);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("modifico"))
                    {
                        resultadoOperacion.exitoso = true;
                    }

                    resultadoOperacion.mensaje = mensajeSP;

                    if (resultadoOperacion.exitoso)
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
                    resultadoOperacion.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultadoOperacion.mensaje = $"Error inesperado: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultadoOperacion;
        }

        public async Task<ResultadoOperacion> EliminarCompetenciaAsync(CompetenciaModelo competenciaModelo)
        {
            var resultadoOperacion = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryEliminarCompetencia, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_id_competencia", competenciaModelo.pkIdCompetencia);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("eliminó") || mensajeSP.Contains("eliminar"))
                    {
                        resultadoOperacion.exitoso = true;
                    }

                    resultadoOperacion.mensaje = mensajeSP;

                    if (resultadoOperacion.exitoso)
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
                    resultadoOperacion.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultadoOperacion.mensaje = $"Error inesperado: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultadoOperacion;
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Funcionario_Periodo_Academico;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class FuncionarioPeriodoAcademicoDato : IFuncionarioPeriodoAcademico
    {
        private readonly string conexion;
        private static readonly string queryMatricularFuncionariosPeriodoAcademico = "matricular_funcionarios_periodo_academico";
        private static readonly string queryMatricularFuncionarioPeriodoAcademico = "matricular_funcionario_periodo_academico";
        private static readonly string queryMatricularEstudiantesPeriodoAcademico = "matricular_estudiante_periodo_academico";
        private static readonly string queryActualizarFechaHabilitiacionFuncionarioPeriodoAcademico = "actualizar_fecha_habilitacion__funcionario_periodo_academico";

        public FuncionarioPeriodoAcademicoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoOperacion> MatricularFuncionariosPeriodoAcademicoAsync(FuncionarioPeriodoAcademicoModelo funcionarioPeriodoAcademicoModelo)
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryMatricularFuncionariosPeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_fecha_inicio_habilitacion", funcionarioPeriodoAcademicoModelo.fechaInicioHabilitacion);
                    comando.Parameters.AddWithValue("@p_fecha_final_habilitacion", funcionarioPeriodoAcademicoModelo.fechaFinalHabilitacion);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("habilitaron"))
                    {
                        resultado.exitoso = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exitoso)
                    {

                        using var comandoDos = new MySqlCommand(queryMatricularEstudiantesPeriodoAcademico, conexion2, transaccion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        await comandoDos.ExecuteNonQueryAsync();

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

        public async Task<ResultadoOperacion> MatricularFuncionarioPeriodoAcademicoAsync(string numeroDocumento, string fechaInicioHabilitacion, string fechaFinalHabilitacion, int idPeriodoAcademico)
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryMatricularFuncionarioPeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);
                    comando.Parameters.AddWithValue("@p_fecha_inicio_habilitacion", fechaInicioHabilitacion);
                    comando.Parameters.AddWithValue("@p_fecha_final_habilitacion", fechaFinalHabilitacion);
                    comando.Parameters.AddWithValue("@p_id_periodo_academico", idPeriodoAcademico);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("vinculó") || mensajeSP.Contains("docente"))
                    {
                        resultado.exitoso = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exitoso)
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

        public async Task<ResultadoOperacion> ActualizarFechaHabiltiacionFuncionarioPeriodoAcademicoAsync(string listaNumeroDocumento, string fechaInicioHabilitacion, string fechaFinalHabilitacion, int idPeriodoAcademico)
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryActualizarFechaHabilitiacionFuncionarioPeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@lista_numero_documento", listaNumeroDocumento);
                    comando.Parameters.AddWithValue("@p_fecha_inicio_habilitacion", fechaInicioHabilitacion);
                    comando.Parameters.AddWithValue("@p_fecha_final_habilitacion", fechaFinalHabilitacion);
                    comando.Parameters.AddWithValue("@p_id_periodo_academico", idPeriodoAcademico);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("actualizaron"))
                    {
                        resultado.exitoso = true;
                    }

                    resultado.mensaje = mensajeSP;

                    if (resultado.exitoso)
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

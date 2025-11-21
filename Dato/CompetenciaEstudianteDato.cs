using Colegio.Interfaz;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class CompetenciaEstudianteDato : ICompetenciaEstudiante
    {
        private readonly string conexion;
        private static readonly string queryCalificarCompetencia = "calificar_competencia";

        public CompetenciaEstudianteDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<bool> CalificarCompetenciaAsync(int idCompetencia, string numeroDocumento, string estadoCompetenciaEstudiante, int idFuncionarioAsignaturaGradoGrupo)
        {
            bool resultado = false;
            MySqlTransaction transaccion = null;

            using var conexion2 = new MySqlConnection(conexion);

            try
            {
                await conexion2.OpenAsync();

                using (transaccion = await conexion2.BeginTransactionAsync())
                {
                    try
                    {
                        using var comando = new MySqlCommand(queryCalificarCompetencia, conexion2, transaccion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        comando.Parameters.AddWithValue("@p_id_competencia", idCompetencia);
                        comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);
                        comando.Parameters.AddWithValue("@p_estado_competencia_estudiante", estadoCompetenciaEstudiante);
                        comando.Parameters.AddWithValue("@p_id_funcionario_asignatura_grado_grupo", idFuncionarioAsignaturaGradoGrupo);

                        int filasAfectadas = await comando.ExecuteNonQueryAsync();

                        if (filasAfectadas > 0)
                        {
                            await transaccion.CommitAsync();
                            resultado = true;
                        }
                        else
                        {
                            await transaccion.RollbackAsync();
                            resultado = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaccion != null)
                        {
                            await transaccion.RollbackAsync();
                        }
                        throw new ApplicationException($"Error al ejecutar la calificación de competencia: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                throw new ApplicationException($"Error de conexión o inicialización de la transacción: {ex.Message}", ex);
            }

            return resultado;
        }
    }
}


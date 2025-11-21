using Colegio.Interfaz;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class EstudiantePeriodoAcademicoDato : IEstudiantePeriodoAcademico
    {
        private readonly string conexion;
        private static readonly string queryPrematricularEstudiantePeriodoAcademico = "prematricular_estudiante_periodo_academico";

        public EstudiantePeriodoAcademicoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoOperacion> PrematricularEstudiantePeriodoAcademico(string numeroDocumento, string nombrePeriodoAcademico)
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
                    using var comando = new MySqlCommand(queryPrematricularEstudiantePeriodoAcademico, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_periodo_academico", nombrePeriodoAcademico);
                    
                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("vinculo") || mensajeSP.Contains("vinculado"))
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

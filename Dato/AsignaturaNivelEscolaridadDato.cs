using Colegio.Interfaz;
using Colegio.Modelos.Asignatura_Nivel_Escolaridad.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class AsignaturaNivelEscolaridadDato : IAsignaturaNivelEscolaridad
    {
        private readonly string conexion;
        private static readonly string queryRegistrarAsignatura = "registrar_asignatura";

        public AsignaturaNivelEscolaridadDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeAsignatuarNivelEscolaridad> RegistrarAsignaturaAsync(string nombreAsignatura, string listaIntensidadHoraria, string listaNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeAsignatuarNivelEscolaridad { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarAsignatura, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);
                    comando.Parameters.AddWithValue("@lista_intensidad_horaria", listaIntensidadHoraria);
                    comando.Parameters.AddWithValue("@lista_nivel_escolaridad", listaNivelEscolaridad);

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
    }
}

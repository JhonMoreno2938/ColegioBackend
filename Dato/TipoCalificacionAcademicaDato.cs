using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Calificacion_Academica;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class TipoCalificacionAcademicaDato : ITipoCalificacionAcademica
    {
        private readonly string conexion;
        private static readonly string queryRegistrarTipoCalificacionAcademica = "registrar_tipo_calificacion_academica";
        private static readonly string queryModificarTipoCalificacionAcademica = "modificar_tipo_calificacion_academica";
        private static readonly string queryListarTipoCalificacionAcademica = "select nombre_tipo_calificacion_academica, valor_porcentaje from listar_tipo_calificaciones_academicas";

        public TipoCalificacionAcademicaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<ResultadoOperacion> RegistrarTipoCalificacionAcademicaAsync(TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo)
        {
            var resultadoOpearcion = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarTipoCalificacionAcademica, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_tipo_calificacion_academica", tipoCalificacionAcademicaModelo.nombreTipoCalificacionAcademica);
                    comando.Parameters.AddWithValue("@p_valor_porcentaje", tipoCalificacionAcademicaModelo.porcentajeModelo.valorPorcentaje);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP != null)
                    {
                        resultadoOpearcion.exitoso = true;
                    }

                    resultadoOpearcion.mensaje = mensajeSP;

                    if (resultadoOpearcion.exitoso)
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
                    resultadoOpearcion.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultadoOpearcion.mensaje = $"Error inesperado: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultadoOpearcion.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultadoOpearcion;
        }

        public async Task<ResultadoOperacion> ModificarTipoCalificacionAcademicaAsync(TipoCalificacionAcademicaModelo tipoCalificacionAcademicaModelo)
        {
            var resultadoOpearcion = new ResultadoOperacion { exitoso = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryModificarTipoCalificacionAcademica, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_tipo_calificacion_academica", tipoCalificacionAcademicaModelo.nombreTipoCalificacionAcademica);
                    comando.Parameters.AddWithValue("@p_valor_porcentaje", tipoCalificacionAcademicaModelo.porcentajeModelo.valorPorcentaje);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP != null)
                    {
                        resultadoOpearcion.exitoso = true;
                    }

                    resultadoOpearcion.mensaje = mensajeSP;

                    if (resultadoOpearcion.exitoso)
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
                    resultadoOpearcion.mensaje = $"Error de base de datos: {ex.Message}";
                }
                catch (Exception ex)
                {
                    await transaccion.RollbackAsync();
                    resultadoOpearcion.mensaje = $"Error inesperado: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultadoOpearcion.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultadoOpearcion;
        }

        public async Task<List<TipoCalificacionAcademicaModelo>> InformacionTipoCalificacionAcademicaAsync()
        {
            var listaTipoCalificacionAcademica = new List<TipoCalificacionAcademicaModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListarTipoCalificacionAcademica, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarTipoCalificacionAcademica = new TipoCalificacionAcademicaModelo();
                                listarTipoCalificacionAcademica.nombreTipoCalificacionAcademica = leer.GetString("nombre_tipo_calificacion_academica");
                                listarTipoCalificacionAcademica.porcentajeModelo.valorPorcentaje = leer["valor_porcentaje"] != DBNull.Value ? Convert.ToInt32(leer["valor_porcentaje"]) : 0;
                                listaTipoCalificacionAcademica.Add(listarTipoCalificacionAcademica);
                            }
                        }
                    }
                }
                return listaTipoCalificacionAcademica;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los RH: {ex.Message}");
                return new List<TipoCalificacionAcademicaModelo>();
            }
        }
    }
}

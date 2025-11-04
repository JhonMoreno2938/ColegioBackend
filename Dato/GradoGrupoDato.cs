using Colegio.Interfaz;
using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Vistas;
using Colegio.Modelos.Jornada.Procedimientos;
using Colegio.Modelos.Jornada.Salidas_Procedimientos;
using Colegio.Modelos.Jornada.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class GradoGrupoDato : IGradoGrupo
    {
        private readonly string conexion;
        private static readonly string queryRegistrarGradoGrupoNivelEscolaridad = "registrar_grado_grupo_nivel_escolaridad";
        private static readonly string queryGestionarGradoGrupo = "gestionar_grado_grupo_nivel_escolaridad";
        private static readonly string queryListaGradoGrupo = "select nombre_grado_grupo, nombre_nivel_escolaridad, estado_grado_grupo from listar_grados_grupos";
        private static readonly string queryListaGradoGrupoActivo = "select nombre_grado_grupo, nombre_nivel_escolaridad from listar_grados_grupos_estado_activo";

        public GradoGrupoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeGradoGrupo> RegistrarGradoGrupoNivelEscolaridadAsync(RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarGradoGrupoNivelEscolaridad, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_grado", registrarGradoGrupoNivelEscolaridad.nombreGrado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", registrarGradoGrupoNivelEscolaridad.nombreGrupo);
                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", registrarGradoGrupoNivelEscolaridad.nombreNivelEscolaridad);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.EndsWith("se registro en el sistema"))
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

        public async Task<ResultadoMensajeGradoGrupo> GestionarEstadoGradoGrupoNivelEscolaridadAsync(GestionarEstadoGradoGrupoNivelEscolaridad gestionarEstadoGradoGrupoNivelEscolaridad)
        {
            var resultado = new ResultadoMensajeGradoGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarGradoGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", gestionarEstadoGradoGrupoNivelEscolaridad.nombreOperacion);
                    comando.Parameters.AddWithValue("@p_nombre_grado", gestionarEstadoGradoGrupoNivelEscolaridad.nombreGrado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", gestionarEstadoGradoGrupoNivelEscolaridad.nombreGrupo);
                    comando.Parameters.AddWithValue("@p_nombre_nivel_escolaridad", gestionarEstadoGradoGrupoNivelEscolaridad.nombreNinvelEscolaridad);

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

        public async Task<List<ListarGradoGrupo>> InformacionGradoGrupoAsync()
        {
            var listaGradoGrupo = new List<ListarGradoGrupo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGradoGrupo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGradoGrupo = new ListarGradoGrupo();
                                listarGradoGrupo.nombreGradoGrupo = leer.GetString("nombre_grado_grupo");
                                listarGradoGrupo.nombreNivelEscolaridad = leer.GetString("nombre_nivel_escolaridad");
                                listarGradoGrupo.estadoGradoGrupo = leer.GetString("estado_grado_grupo");
                                listaGradoGrupo.Add(listarGradoGrupo);
                            }
                        }
                    }
                }
                return listaGradoGrupo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grados grupos: {ex.Message}");
                return new List<ListarGradoGrupo>();
            }
        }

        public async Task<List<ListarGradoGrupoEstadoActivo>> InformacionGradoGrupoEstadoActivoAsync()
        {
            var listaGradoGrupoEstadoActivo = new List<ListarGradoGrupoEstadoActivo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGradoGrupoActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGradoGrupoEstadoActivo = new ListarGradoGrupoEstadoActivo();
                                listarGradoGrupoEstadoActivo.nombreGradoGrupo = leer.GetString("nombre_grado_grupo");
                                listarGradoGrupoEstadoActivo.nombreNivelEscolaridad = leer.GetString("nombre_nivel_escolaridad");
                                listaGradoGrupoEstadoActivo.Add(listarGradoGrupoEstadoActivo);
                            }
                        }
                    }
                }
                return listaGradoGrupoEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grados grupos: {ex.Message}");
                return new List<ListarGradoGrupoEstadoActivo>();
            }
        }
    }
}

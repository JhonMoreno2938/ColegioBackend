using Colegio.Interfaz;
using Colegio.Modelos.Grupo.Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Modelos.Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grupo.Vistas;

namespace Colegio.Dato
{
    public class GrupoDato : IGrupo
    {
        private readonly string conexion;
        private static readonly string queryRegistrarGrupo = "registrar_grupo";
        private static readonly string queryGestionarEstadoGrupo = "gestionar_estado_grupo";
        private static readonly string queryListaGrupo = "select nombre_grupo, estado_grupo from listar_grupos";
        private static readonly string queryListaGrupoActivo = "select nombre_grupo from listar_grupos_estado_activo";

        public GrupoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoMensajeGrupo> RegistrarGrupoAsync(RegistrarGrupo registrarGrupo)
        {
            var resultado = new ResultadoMensajeGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_grupo", registrarGrupo.nombreGrupo);

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

        public async Task<ResultadoMensajeGrupo> GestionarEstadoGrupoAsync(GestionarEstadoGrupo gestionarEstadoGrupo)
        {
            var resultado = new ResultadoMensajeGrupo { exito = false, mensaje = "Error de conexión/ejecución no capturado." };

            MySqlTransaction transaccion = null;

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryGestionarEstadoGrupo, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_operacion", gestionarEstadoGrupo.nombreOperacion);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", gestionarEstadoGrupo.nombreGrupo);

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

        public async Task<List<ListarGrupo>> InformacionGrupoAsync()
        {
            var listaGrupo = new List<ListarGrupo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGrupo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGrupo = new ListarGrupo();
                                listarGrupo.nombreGrupo = leer.GetString("nombre_grupo");
                                listarGrupo.estadoGrupo = leer.GetString("estado_grupo");
                                listaGrupo.Add(listarGrupo);
                            }
                        }
                    }
                }
                return listaGrupo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grupos: {ex.Message}");
                return new List<ListarGrupo>();
            }
        }

        public async Task<List<ListarGrupoEstadoActivo>> InformacionGrupoEstadoActivoAsync()
        {
            var listaGrupoEstadoActivo = new List<ListarGrupoEstadoActivo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGrupoActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGrupoEstadoActivo = new ListarGrupoEstadoActivo();
                                listarGrupoEstadoActivo.nombreGrupo = leer.GetString("nombre_grupo");
                                listaGrupoEstadoActivo.Add(listarGrupoEstadoActivo);
                            }
                        }
                    }
                }
                return listaGrupoEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grupos: {ex.Message}");
                return new List<ListarGrupoEstadoActivo>();
            }
        }
    }
}

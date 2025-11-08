using Colegio.Interfaz;
using Colegio.Modelos.Asignatura;
using Colegio.Modelos.Asignatura.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class AsignaturaDato : IAsignatura
    {
        private readonly string conexion;
        private static readonly string queryConsultarAsginatura = "consultar_asignatura";
        private static readonly string queryConsultarGradoGrpoAsignaturaEstadoActivo = "consultar_grado_grupo_asignatura_estado_activo";
        private static readonly string queryListaAsignatura = "select nombre_asignatura, estado_asignatura from listar_asignaturas";
        private static readonly string queryListaAsignaturaActivo = "select nombre_asignatura from listar_asignaturas_estado_activo";

        public AsignaturaDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<SalidaConsultarAsignatura> ConsultarAsignaturaAsync(string nombreAsignatura)
        {
            var resultado = new SalidaConsultarAsignatura() { exito = false };

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarAsginatura, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);

                    using var leer = await comando.ExecuteReaderAsync();

                    // Proceda el primer select de la consulta.

                    if (await leer.ReadAsync())
                    {
                        resultado.nombreAsignatura = leer["nombre_asignatura"]?.ToString() ?? string.Empty;

                        resultado.exito = true;
                    }
                    else
                    {
                        resultado.mensaje = "Asignatura no encontrada o nombre asignatura incorrecto.";
                        return resultado;
                    }

                    // Procesa el segundo select de la consulta.

                    if (await leer.NextResultAsync())
                    {
                        while (await leer.ReadAsync())
                        {
                            resultado.gradosGruposVinculados.Add(new InformacionConsultarAsignatura
                            {
                                nombreNivelEscolaridad = leer["nombre_nivel_escolaridad"]?.ToString() ?? string.Empty,
                                intensidadHoraria = leer["intensidad_horaria"] != DBNull.Value ? Convert.ToInt32(leer["intensidad_horaria"]): 0,
                                nombreGradoGrupo = leer["grado_grupo"]?.ToString() ?? string.Empty,
                                estadoAsignaturaGradoGrupo = leer["estado_asignatura_grado_grupo"]?.ToString() ?? string.Empty

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

        public async Task<List<SalidaConsultarGradoGrupoAsignaturaEstadoActivo>> ConsultarGradoGrupoAsignaturaEstadoActivoAsync(string nombreAsignatura)
        {
            var listaResultados = new List<SalidaConsultarGradoGrupoAsignaturaEstadoActivo>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarGradoGrpoAsignaturaEstadoActivo, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_asignatura", nombreAsignatura);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {

                        var item = new SalidaConsultarGradoGrupoAsignaturaEstadoActivo();

                        item.nombreGradoGrupo = leer["grado_grupo"]?.ToString() ?? string.Empty;
                        item.nombreSede = leer["nombre_sede"]?.ToString() ?? string.Empty;
                        item.nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty;


                        listaResultados.Add(item);
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

        public async Task<List<AsignaturaModelo>> InformacionAsignaturaAsync()
        {
            var listaAsignatura = new List<AsignaturaModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaAsignatura, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarAsignatura = new AsignaturaModelo();
                                listarAsignatura.nombreAsignatura = leer.GetString("nombre_asignatura");
                                listarAsignatura.estadoAsignatura = leer.GetString("estado_asignatura");
                                listaAsignatura.Add(listarAsignatura);
                            }
                        }
                    }
                }
                return listaAsignatura;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las asignaturas: {ex.Message}");
                return new List<AsignaturaModelo>();
            }
        }

        public async Task<List<AsignaturaModelo>> InformacionAsignaturaEstadoActivoAsync()
        {

            var listaAsignaturaEstadoActivo = new List<AsignaturaModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaAsignaturaActivo, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarAsignaturaEstadoActivo = new AsignaturaModelo();
                                listarAsignaturaEstadoActivo.nombreAsignatura = leer.GetString("nombre_asignatura");
                                listaAsignaturaEstadoActivo.Add(listarAsignaturaEstadoActivo);
                            }
                        }
                    }
                }
                return listaAsignaturaEstadoActivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las asignaturas: {ex.Message}");
                return new List<AsignaturaModelo>();
            }
        }
    }
}

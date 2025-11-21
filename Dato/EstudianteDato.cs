using Colegio.Interfaz;
using Colegio.Modelos.Estudiante;
using Colegio.Modelos.Estudiante.Consultas;
using Colegio.Modelos.Estudiante.Vistas;
using Colegio.Utilidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class EstudianteDato : IEstudiante
    {
        private readonly string conexion;
        private static readonly string queryRegistrarEstudiante = "registrar_estudiante";
        private static readonly string queryCargueEstudianteTablaTemporal = "cargue_estudiante_tabla_temporal";
        private static readonly string queryProcesarCargueEstudiante = "procesar_cargue_estudiante";
        private static readonly string queryModificarInformacionEstudiante = "modificar_informacion_estudiante";
        private static readonly string queryConsultarEstudiante = "consultar_estudiante";
        private static readonly string queryListaEstudiante = "select nombre_completo, numero_documento_persona, codigo_estudiante, estado_estudiante," +
            "nombre_grado_grupo, nombre_sede, nombre_jornada from listar_estudiantes";


        public EstudianteDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<ResultadoConID> RegistrarEstudianteAsync(EstudianteModelo estudianteModelo, string sede, string jornada, string grado, string grupo)
        {
            var resultado = new ResultadoConID();
            MySqlTransaction transaccion = null;
            try
            {

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarEstudiante, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_primer_nombre", estudianteModelo.personaModelo.primerNombrePersona);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", string.IsNullOrEmpty(estudianteModelo.personaModelo.segundoNombrePersona) ? (object)DBNull.Value : estudianteModelo.personaModelo.segundoNombrePersona);
                    comando.Parameters.AddWithValue("@p_primer_apellido", estudianteModelo.personaModelo.primerApellidoPersona);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", string.IsNullOrEmpty(estudianteModelo.personaModelo.segundoApellidoPersona) ? (object)DBNull.Value : estudianteModelo.personaModelo.segundoApellidoPersona);
                    comando.Parameters.AddWithValue("@p_numero_documento", estudianteModelo.personaModelo.numeroDocumentoPersona);
                    comando.Parameters.AddWithValue("@p_fecha_nacimiento", estudianteModelo.personaModelo.fechaNacimientoPersona);
                    comando.Parameters.AddWithValue("@p_edad", estudianteModelo.personaModelo.edadPersona);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_documento", estudianteModelo.personaModelo.tipoDocumentoModelo.nombreTipoDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_genero", estudianteModelo.personaModelo.generoModelo.nombreGenero);
                    comando.Parameters.AddWithValue("@p_nombre_departamento_nacimiento", estudianteModelo.personaModelo.departamentoNacimientoModelo.nombreDepartamento);
                    comando.Parameters.AddWithValue("@p_nombre_departamento_expedicion_documento", estudianteModelo.personaModelo.departamentoExpedicionDocumentoModelo.nombreDepartamento);
                    comando.Parameters.AddWithValue("@p_ciudad_nacimiento", estudianteModelo.personaModelo.ciudadNacimientoModelo.nombreCiudad);
                    comando.Parameters.AddWithValue("@p_ciudad_expedicion_documento", estudianteModelo.personaModelo.ciudadExpedicionModelo.nombreCiudad);
                    comando.Parameters.AddWithValue("@p_nombre_rh", estudianteModelo.personaModelo.rhModelo.nombreRh);
                    comando.Parameters.AddWithValue("@p_nombre_EPS", estudianteModelo.epsModelo.nombreEps);
                    comando.Parameters.AddWithValue("@p_nombre_estrato_social", estudianteModelo.estratoSocialModelo.nombreEstratoSocial);
                    comando.Parameters.AddWithValue("@p_nombre_discapacidad", estudianteModelo.discapacidadModelo.nombreDiscapacidad);
                    comando.Parameters.AddWithValue("@p_nombre_sisben", estudianteModelo.sisbenModelo.nombreSisben);
                    comando.Parameters.AddWithValue("@p_nombre_sede", sede);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", jornada);
                    comando.Parameters.AddWithValue("@p_nombre_grado", grado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", grupo);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    var outMensajeId = comando.Parameters.Add("@mensaje_id", MySqlDbType.Int32);
                    outMensajeId.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";
                    string idGenerada = outMensajeId.Value is DBNull ? "0" : outMensajeId.Value.ToString() ?? "0";

                    resultado.mensajeId = Convert.ToInt32(idGenerada);
                    resultado.mensaje = mensajeSP;

                    if (int.TryParse(idGenerada, out int idValido) && idValido > 0)
                    {
                        resultado.exitoso = true;
                    }

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

        public async Task<ResultadoOperacion> CargueEstudianteCSVAsync(EstudianteModelo estudianteModelo, string nombreTipoDocumento, string sede, string jornada, string grado, string grupo, int annioActual)
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error en el servidor o la base de datos." };
            MySqlTransaction transaccion = null;

            try
            {
                string contraseinaEncriptada = Seguridad.EncriptarContraseina(estudianteModelo.personaModelo.numeroDocumentoPersona, estudianteModelo.personaModelo.numeroDocumentoPersona);

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryCargueEstudianteTablaTemporal, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_primer_nombre", estudianteModelo.personaModelo.primerNombrePersona);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", string.IsNullOrEmpty(estudianteModelo.personaModelo.segundoNombrePersona) ? (object)DBNull.Value : estudianteModelo.personaModelo.segundoNombrePersona);
                    comando.Parameters.AddWithValue("@p_primer_apellido", estudianteModelo.personaModelo.primerApellidoPersona);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", string.IsNullOrEmpty(estudianteModelo.personaModelo.segundoApellidoPersona) ? (object)DBNull.Value : estudianteModelo.personaModelo.segundoApellidoPersona);
                    comando.Parameters.AddWithValue("@p_numero_documento", estudianteModelo.personaModelo.numeroDocumentoPersona);
                    comando.Parameters.AddWithValue("@p_fecha_nacimiento", estudianteModelo.personaModelo.fechaNacimientoPersona);
                    comando.Parameters.AddWithValue("@p_edad", estudianteModelo.personaModelo.edadPersona);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_documento", nombreTipoDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_genero", estudianteModelo.personaModelo.generoModelo.nombreGenero);
                    comando.Parameters.AddWithValue("@p_nombre_rh", estudianteModelo.personaModelo.rhModelo.nombreRh);
                    comando.Parameters.AddWithValue("@p_codigo_estudiante", estudianteModelo.codigoEstudiante);
                    comando.Parameters.AddWithValue("@p_estado_estudiante", estudianteModelo.estadoEstudiante);
                    comando.Parameters.AddWithValue("@p_nombre_EPS", estudianteModelo.epsModelo.nombreEps);
                    comando.Parameters.AddWithValue("@p_nombre_estrato_social", estudianteModelo.estratoSocialModelo.nombreEstratoSocial);
                    comando.Parameters.AddWithValue("@p_nombre_discapacidad", estudianteModelo.discapacidadModelo.nombreDiscapacidad);
                    comando.Parameters.AddWithValue("@p_nombre_sisben", estudianteModelo.sisbenModelo.nombreSisben);
                    comando.Parameters.AddWithValue("@p_contraseina_usuario", contraseinaEncriptada);
                    comando.Parameters.AddWithValue("@p_nombre_sede", sede);
                    comando.Parameters.AddWithValue("@p_nombre_jornada", jornada);
                    comando.Parameters.AddWithValue("@p_nombre_grado", grado);
                    comando.Parameters.AddWithValue("@p_nombre_grupo", grupo);
                    comando.Parameters.AddWithValue("@p_annio_actual", annioActual);


                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        resultado.exitoso = true;
                        resultado.mensaje = "El estudiante fue registrado exitosamente.";
                    }
                    else
                    {
                        if (estudianteModelo.estadoEstudiante.Equals("RETIRADO", StringComparison.OrdinalIgnoreCase))
                        {
                            resultado.exitoso = true;
                            resultado.mensaje = $"Omisión intencional: Estudiante con estado '{estudianteModelo.estadoEstudiante}' no fue registrado.";
                        }
                        else
                        {
                            resultado.mensaje = "La operación se ejecutó, pero no se afectaron registros. Verifique los datos de entrada.";
                        }
                    }

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
                    if (transaccion != null) await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error de base de datos (SQL): {ex.Message}";
                }
                catch (Exception ex)
                {
                    if (transaccion != null) await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error inesperado del servidor: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                resultado.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return resultado;
        }

        public async Task<ResultadoOperacion> ModificarInformacionEstudianteAsync(EstudianteModelo estudianteModelo)
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
                    using var comando = new MySqlCommand(queryModificarInformacionEstudiante, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", estudianteModelo.personaModelo.numeroDocumentoPersona);
                    comando.Parameters.AddWithValue("@p_primer_nombre", estudianteModelo.personaModelo.primerNombrePersona);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", estudianteModelo.personaModelo.segundoNombrePersona);
                    comando.Parameters.AddWithValue("@p_primer_apellido", estudianteModelo.personaModelo.primerApellidoPersona);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", estudianteModelo.personaModelo.segundoApellidoPersona);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_documento", estudianteModelo.personaModelo.tipoDocumentoModelo.nombreTipoDocumento);
                    comando.Parameters.AddWithValue("@p_departamento_nacimiento", estudianteModelo.personaModelo.departamentoNacimientoModelo.nombreDepartamento);
                    comando.Parameters.AddWithValue("@p_ciudad_nacimiento", estudianteModelo.personaModelo.ciudadNacimientoModelo.nombreCiudad);
                    comando.Parameters.AddWithValue("@p_departamento_expedicion", estudianteModelo.personaModelo.departamentoExpedicionDocumentoModelo.nombreDepartamento);
                    comando.Parameters.AddWithValue("@p_ciudad_expedicion", estudianteModelo.personaModelo.ciudadExpedicionModelo.nombreCiudad);
                    comando.Parameters.AddWithValue("@p_nombre_EPS", estudianteModelo.epsModelo.nombreEps);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.StartsWith("Se actualizo"))
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

        public async Task<ConsultarEstudiante> ConsultarEstudianteAsync(string numeroDocumento)
        {
            var consultarEstudiante = new ConsultarEstudiante();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarEstudiante, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);

                    using var leer = await comando.ExecuteReaderAsync();

                    // Proceda el primer select de la consulta.

                    if (await leer.ReadAsync())
                    {
                        consultarEstudiante.primerNombre = leer["primer_nombre_persona"]?.ToString() ?? string.Empty;
                        consultarEstudiante.segundoNombre = leer["segundo_nombre_persona"]?.ToString() ?? string.Empty;
                        consultarEstudiante.primerApellido = leer["primer_apellido_persona"]?.ToString() ?? string.Empty;
                        consultarEstudiante.segundoApellido = leer["segundo_apellido_persona"]?.ToString() ?? string.Empty;
                        consultarEstudiante.numeroDocumento = leer["numero_documento_persona"]?.ToString() ?? string.Empty;
                        string fechaNacimientoPersona = leer["fecha_nacimiento"]?.ToString() ?? string.Empty;
                        consultarEstudiante.fechaNacimiento = fechaNacimientoPersona.Length >= 10 ? fechaNacimientoPersona.Substring(0, 10) : fechaNacimientoPersona;
                        consultarEstudiante.edad = leer["edad_persona"] != DBNull.Value ? Convert.ToInt32(leer["edad_persona"]) : 0;
                        consultarEstudiante.nombreTipoDocumento = leer["nombre_tipo_documento"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreGenero = leer["nombre_genero"]?.ToString() ?? string.Empty;
                        consultarEstudiante.departamentoNacimiento = leer["departamento_nacimiento"]?.ToString() ?? string.Empty;
                        consultarEstudiante.departamentoExpedicion = leer["departamento_expedicion_documento"]?.ToString() ?? string.Empty;
                        consultarEstudiante.ciudadNacimiento = leer["ciudad_nacimiento"]?.ToString() ?? string.Empty;
                        consultarEstudiante.ciudadExpedicion = leer["ciudad_expedicion"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreRh = leer["nombre_rh"]?.ToString() ?? string.Empty;
                        consultarEstudiante.codigoEstudiante = leer["codigo_estudiante"]?.ToString() ?? string.Empty;
                        consultarEstudiante.estadoEstudiante = leer["estado_estudiante"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreEps = leer["nombre_EPS"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreEstratoSocial = leer["nombre_estrato_social"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreDiscapacidad = leer["nombre_discapacidad"]?.ToString() ?? string.Empty;
                        consultarEstudiante.nombreSisben = leer["nombre_sisben"]?.ToString() ?? string.Empty;

                        consultarEstudiante.exito = true;
                    }
                    else
                    {
                        consultarEstudiante.mensaje = "Numero de documento no encontrado o el estudianten no existe.";
                        return consultarEstudiante;
                    }

                    // Procesa el segundo select de la consulta.

                    if (await leer.NextResultAsync())
                    {
                        while (await leer.ReadAsync())
                        {
                            object rawDateValue = leer["fecha_inscripcion"];
                            string fechaInscripcionLimpia = string.Empty;

                            string strDate = rawDateValue.ToString() ?? string.Empty;
                            fechaInscripcionLimpia = strDate.Length >= 10 ? strDate.Substring(0, 10) : strDate;

                            consultarEstudiante.gradosGruposVinculados.Add(new ConsultarEstudianteAcademico
                            {
                                nombreGradoGrupo = leer["nombre_grado_grupo"]?.ToString() ?? string.Empty,
                                nombreSede = leer["nombre_sede"]?.ToString() ?? string.Empty,
                                nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty,
                                fechaInscripcionGradoGrupo = fechaInscripcionLimpia,
                                estadoEstudianteSedeJornadaGradoGrupo = leer["estado_estudiante_sede_jornada_grado_grupo"]?.ToString() ?? string.Empty,

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
                consultarEstudiante.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return consultarEstudiante;

        }

        public async Task<List<ListarEstudiante>> InformacionEstudianteAsync()
        {
            var listaEstudiante = new List<ListarEstudiante>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaEstudiante, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarEstudiante = new ListarEstudiante();
                                listarEstudiante.nombreCompleto = leer.GetString("nombre_completo");
                                listarEstudiante.numeroDocumento = leer.GetString("numero_documento_persona");
                                listarEstudiante.codigoEstudiante = leer.GetString("codigo_estudiante");
                                listarEstudiante.estadoEstudiante = leer.GetString("estado_estudiante");
                                listarEstudiante.nombreGradoGrupo = leer.GetString("nombre_grado_grupo");
                                listarEstudiante.nombreSede = leer.GetString("nombre_sede");
                                listarEstudiante.nombreJornada = leer.GetString("nombre_jornada");
                                listaEstudiante.Add(listarEstudiante);
                            }
                        }
                    }
                }
                return listaEstudiante;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estudiantes: {ex.Message}");
                return new List<ListarEstudiante>();
            }
        }

        public async Task<ResultadoOperacion> ProcesarCargueEstudianteAsync()
        {
            var resultado = new ResultadoOperacion { exitoso = false, mensaje = "Error en el servidor o la base de datos." };
            MySqlTransaction transaccion = null;

            try
            {

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryProcesarCargueEstudiante, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    int filasAfectadas = await comando.ExecuteNonQueryAsync();

                    if (filasAfectadas > 0)
                    {
                        resultado.exitoso = true;
                        resultado.mensaje = $"Cargue masivo completado. {filasAfectadas} registros procesados.";
                    }
                    else
                    {
                        resultado.exitoso = true; 
                        resultado.mensaje = "Proceso completado, pero no se procesó ningún estudiante (tabla temporal vacía).";
                    }


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
                    if (transaccion != null) await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error de base de datos (SQL): {ex.Message}";
                }
                catch (Exception ex)
                {
                    if (transaccion != null) await transaccion.RollbackAsync();
                    resultado.mensaje = $"Error inesperado del servidor: {ex.Message}";
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

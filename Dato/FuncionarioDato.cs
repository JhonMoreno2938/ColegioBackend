using Colegio.Interfaz;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Utilidades;
using Colegio.Modelos.Funcionario;
using Colegio.Modelos.Funcionario.Consultas;
using Colegio.Modelos.Funcionario.Vistas;
using Colegio.Modelos.Funcionario.Procedimientos;

namespace Colegio.Dato
{
    public class FuncionarioDato : IFuncionario
    {
        private readonly string conexion;
        private static readonly string queryRegistrarFuncionario = "registrar_funcionario";
        private static readonly string queryModificarInformacionFuncionario = "modificar_informacion_funcionario";
        private static readonly string queryConsultarFuncionario = "consultar_funcionario";
        private static readonly string queryListarFuncionario = "select nombre_completo, numero_documento, sedes_asignadas, jornadas_asignadas, estado_funcionario from listar_funcionarios";
        private static readonly string queryConsultarGradoGrupoFuncionarioEstadoActivo = "consultar_grados_grupos_funcionario_estado_activo";
        private static readonly string queryConsultarCompetenciaFuncionario = "consultar_competencia_funcionario";

        public FuncionarioDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(FuncionarioModelo funcionarioModelo)
        {
            var resultado = new SalidaRegistrarFuncionario() { exito = false };

            MySqlTransaction transaccion = null;

            try
            {
                string contraseinaEncriptada = Seguridad.EncriptarContraseina(funcionarioModelo.personaModelo.numeroDocumentoPersona, funcionarioModelo.personaModelo.numeroDocumentoPersona);

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryRegistrarFuncionario, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_primer_nombre", funcionarioModelo.personaModelo.primerNombrePersona);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", string.IsNullOrEmpty(funcionarioModelo.personaModelo.segundoNombrePersona) ? (object)DBNull.Value : funcionarioModelo.personaModelo.segundoNombrePersona);
                    comando.Parameters.AddWithValue("@p_primer_apellido", funcionarioModelo.personaModelo.primerApellidoPersona);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", string.IsNullOrEmpty(funcionarioModelo.personaModelo.segundoApellidoPersona) ? (object)DBNull.Value : funcionarioModelo.personaModelo.segundoApellidoPersona);
                    comando.Parameters.AddWithValue("@p_numero_documento", funcionarioModelo.personaModelo.numeroDocumentoPersona);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_documento", funcionarioModelo.personaModelo.tipoDocumentoModelo.nombreTipoDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_genero", funcionarioModelo.personaModelo.generoModelo.nombreGenero);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_funcionario", funcionarioModelo.tipoFuncionarioModelo.nombreTipoFuncionario);
                    comando.Parameters.AddWithValue("@p_contraseina_usuario", contraseinaEncriptada);

                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    var outMensajeId = comando.Parameters.Add("@mensaje_id", MySqlDbType.Int32);
                    outMensajeId.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";
                    string idGenerada = outMensajeId.Value is DBNull ? "0" : outMensajeId.Value.ToString() ?? "0";

                    resultado.mensajeId = idGenerada;
                    resultado.mensaje = mensajeSP;

                    if (int.TryParse(idGenerada, out int idValido) && idValido > 0)
                    {
                        resultado.exito = true;
                    }

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

        public async Task<ResultadoOperacion> ModificarInformacionFuncionarioAsync(FuncionarioModelo funcionarioModelo)
        {
            var resultadoOperacion = new ResultadoOperacion();

            MySqlTransaction transaccion = null;

            try
            {

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();

                try
                {
                    using var comando = new MySqlCommand(queryModificarInformacionFuncionario, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_primer_nombre", funcionarioModelo.personaModelo.primerNombrePersona);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", string.IsNullOrEmpty(funcionarioModelo.personaModelo.segundoNombrePersona) ? (object)DBNull.Value : funcionarioModelo.personaModelo.segundoNombrePersona);
                    comando.Parameters.AddWithValue("@p_primer_apellido", funcionarioModelo.personaModelo.primerApellidoPersona);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", string.IsNullOrEmpty(funcionarioModelo.personaModelo.segundoApellidoPersona) ? (object)DBNull.Value : funcionarioModelo.personaModelo.segundoApellidoPersona);
                    comando.Parameters.AddWithValue("@p_numero_documento", funcionarioModelo.personaModelo.numeroDocumentoPersona);


                    var outMensaje = comando.Parameters.Add("@mensaje", MySqlDbType.VarChar, 255);
                    outMensaje.Direction = ParameterDirection.Output;

                    await comando.ExecuteNonQueryAsync();

                    string mensajeSP = outMensaje.Value?.ToString() ?? "Error desconocido en el SP.";

                    if (mensajeSP.Contains("Se modifico"))
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

        public async Task<ConsultarFuncionario> ConsultarFuncionarioAsync(string numeroDocumento)
        {
            var consultarFuncionario = new ConsultarFuncionario();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarFuncionario, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_numero_documento", numeroDocumento);

                    using var leer = await comando.ExecuteReaderAsync();

                    // Proceda el primer select de la consulta.

                    if (await leer.ReadAsync())
                    {
                        consultarFuncionario.primerNombre = leer["primer_nombre_persona"]?.ToString() ?? string.Empty;
                        consultarFuncionario.segundoNombre = leer["segundo_nombre_persona"]?.ToString() ?? string.Empty;
                        consultarFuncionario.primerApellido = leer["primer_apellido_persona"]?.ToString() ?? string.Empty;
                        consultarFuncionario.segundoApellido = leer["segundo_apellido_persona"]?.ToString() ?? string.Empty;
                        consultarFuncionario.numeroDocumento = leer["numero_documento_persona"]?.ToString() ?? string.Empty;
                        consultarFuncionario.nombreTipoDocumento = leer["nombre_tipo_documento"]?.ToString() ?? string.Empty;
                        
                        consultarFuncionario.exito = true;
                    }
                    else
                    {
                        consultarFuncionario.mensaje = "Numero de documento no encontrado o el funcionario no existe.";
                        return consultarFuncionario;
                    }

                    // Procesa el segundo select de la consulta.

                    if (await leer.NextResultAsync())
                    {
                        while (await leer.ReadAsync())
                        {

                            consultarFuncionario.gradosGruposVinculados.Add(new ConsultarFuncionarioAcademico
                            {
                                idFuncionarioAsignaturaGradoGrupo = leer["pk_id_funcionario_asignatura_grado_grupo"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_funcionario_asignatura_grado_grupo"]) : 0,
                                estadoFuncionarioAsignaturaGradoGrupo = leer["estado_funcionario_asignatura_grado_grupo"]?.ToString() ?? string.Empty,
                                nombreAsignatura = leer["nombre_asignatura"]?.ToString() ?? string.Empty,
                                nombreGradoGrupo = leer["nombre_grado_grupo"]?.ToString() ?? string.Empty,
                                nombreSede = leer["nombre_sede"]?.ToString() ?? string.Empty,
                                nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty,
                               
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
                consultarFuncionario.mensaje = $"Error de conexión a la base de datos: {ex.Message}";
            }

            return consultarFuncionario;
        }

        public async Task<List<ListarFuncionario>> InformacionFuncionarioAsync()
        {
            var listaFuncionario = new List<ListarFuncionario>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListarFuncionario, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarFuncionario = new ListarFuncionario();
                                listarFuncionario.nombreCompleto = leer.GetString("nombre_completo");
                                listarFuncionario.numeroDocumento = leer.GetString("numero_documento");
                                listarFuncionario.nombreSede = leer.GetString("sedes_asignadas");
                                listarFuncionario.nombreJornada = leer.GetString("jornadas_asignadas");
                                listarFuncionario.estadoFuncionario = leer.GetString("estado_funcionario");
                                listaFuncionario.Add(listarFuncionario);
                            }
                        }
                    }
                }
                return listaFuncionario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estudiantes: {ex.Message}");
                return new List<ListarFuncionario>();
            }
        }

        public async Task<List<ConsultarGradoGrupoFuncionarioEstadoActivo>> InformacionGradoGrupoFuncionarioEstadoActivoAsync(string nombreUsuario)
        {
            var consultarGradoGrupoFuncionarioEstadoActivo = new List<ConsultarGradoGrupoFuncionarioEstadoActivo>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarGradoGrupoFuncionarioEstadoActivo, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_usuario", nombreUsuario);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {
                        var consultarGradosGruposFuncionariosEstadoActivo = new ConsultarGradoGrupoFuncionarioEstadoActivo
                        {
                            idFuncionarioAsignaturaGradoGrupo = leer["pk_id_funcionario_asignatura_grado_grupo"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_funcionario_asignatura_grado_grupo"]) : 0,
                            nombreAsignatura = leer["nombre_asignatura"]?.ToString() ?? string.Empty,
                            nombreGradoGrupo = leer["nombre_grado_grupo"]?.ToString() ?? string.Empty,
                            nombreJornada = leer["nombre_jornada"]?.ToString() ?? string.Empty,
                            nombreSede = leer["nombre_sede"]?.ToString() ?? string.Empty,

                        };

                        consultarGradoGrupoFuncionarioEstadoActivo.Add(consultarGradosGruposFuncionariosEstadoActivo);

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

            return consultarGradoGrupoFuncionarioEstadoActivo; // Retorna el DTO con datos o null.
        }

        public async Task<List<ConsultarCompetenciaFuncionario>> ConsultarCompetenciaFuncionarioAsync(string nombreUsuario)
        {
            var consultarCompetenciaFuncionario = new List<ConsultarCompetenciaFuncionario>();

            try
            {
                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                try
                {
                    using var comando = new MySqlCommand(queryConsultarCompetenciaFuncionario, conexion2)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_nombre_usuario", nombreUsuario);

                    using var leer = await comando.ExecuteReaderAsync();

                    while (await leer.ReadAsync())
                    {
                        var consultarCompetenciasFuncionario = new ConsultarCompetenciaFuncionario
                        {
                            
                            nombreAsignatura = leer["nombre_asignatura"]?.ToString() ?? string.Empty,
                            idCompetencia = leer["pk_id_competencia"] != DBNull.Value ? Convert.ToInt32(leer["pk_id_competencia"]) : 0,
                            descripcionCompetencia = leer["descripcion_competencia"]?.ToString() ?? string.Empty,
                            nombreGradoGrupo = leer["nombre_grado_grupo"]?.ToString() ?? string.Empty,
                            nombrePeriodoAcademico = leer["nombre_periodo_academico"]?.ToString() ?? string.Empty
                        };

                        consultarCompetenciaFuncionario.Add(consultarCompetenciasFuncionario);

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

            return consultarCompetenciaFuncionario; // Retorna el DTO con datos o null.
        }
    }
}

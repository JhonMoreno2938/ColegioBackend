using Colegio.Interfaz;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Utilidades;
using Colegio.Modelos.Funcionario;


namespace Colegio.Dato
{
    public class FuncionarioDato : IFuncionario
    {
        private readonly string conexion;
        private static readonly string queryRegistrarFuncionario = "registrar_funcionario";

        public FuncionarioDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                               ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(FuncionarioModelo funcionarioModelo)
        {
            var resultado = new SalidaRegistrarFuncionario() { exito = false};

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
    }
}

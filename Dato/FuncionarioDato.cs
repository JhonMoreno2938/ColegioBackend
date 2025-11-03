using Colegio.Interfaz;
using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using MySql.Data.MySqlClient;
using System.Data;
using Colegio.Utilidades;


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

        public async Task<SalidaRegistrarFuncionario> RegistrarFuncionarioAsync(RegistrarFuncionario registrarFuncionario)
        {
            var resultado = new SalidaRegistrarFuncionario() { exito = false};

            MySqlTransaction transaccion = null;

            try
            {
                string contraseinaEncriptada = Seguridad.EncriptarContraseina(registrarFuncionario.numeroDocumento, registrarFuncionario.numeroDocumento);

                using var conexion2 = new MySqlConnection(conexion);
                await conexion2.OpenAsync();

                transaccion = await conexion2.BeginTransactionAsync();
               
                try
                {
                    using var comando = new MySqlCommand(queryRegistrarFuncionario, conexion2, transaccion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@p_primer_nombre", registrarFuncionario.primerNombre);
                    comando.Parameters.AddWithValue("@p_segundo_nombre", string.IsNullOrEmpty(registrarFuncionario.segundoNombre) ? (object)DBNull.Value : registrarFuncionario.segundoNombre);
                    comando.Parameters.AddWithValue("@p_primer_apellido", registrarFuncionario.primerApellido);
                    comando.Parameters.AddWithValue("@p_segundo_apellido", string.IsNullOrEmpty(registrarFuncionario.segundoApellido) ? (object)DBNull.Value : registrarFuncionario.segundoApellido);
                    comando.Parameters.AddWithValue("@p_numero_documento", registrarFuncionario.numeroDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_documento", registrarFuncionario.nombreTipoDocumento);
                    comando.Parameters.AddWithValue("@p_nombre_genero", registrarFuncionario.nombreGenero);
                    comando.Parameters.AddWithValue("@p_nombre_tipo_funcionario", registrarFuncionario.nombreTipoFuncionario);
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

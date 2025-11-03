using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Funcionario.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class TipoFuncionarioDato : ITipoFuncionario
    {
        private readonly string conexion;
        private static readonly string queryListaTipoFuncionario = "select nombre_tipo_funcionario from listar_tipos_funcionarios";

        public TipoFuncionarioDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }
        public async Task<List<ListarTipoFuncionario>> InformacionTipoFuncionarioAsync()
        {
            var listaTipoFuncionario = new List<ListarTipoFuncionario>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaTipoFuncionario, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarTipoFuncionario = new ListarTipoFuncionario();
                                listarTipoFuncionario.nombreTipoFuncionario = leer.GetString("nombre_tipo_funcionario");
                                listaTipoFuncionario.Add(listarTipoFuncionario);
                            }
                        }
                    }
                }
                return listaTipoFuncionario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de funcionarios: {ex.Message}");
                return new List<ListarTipoFuncionario>();
            }
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Departamento;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class DepartamentoDato : IDepartamento
    {
        private readonly string conexion;
        private static readonly string queryListaDepartamento = "select nombre_departamento from listar_departamentos";

        public DepartamentoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<DepartamentoModelo>> InformacionDepartamentoAsync()
        {
            var listaDepartamento = new List<DepartamentoModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaDepartamento, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarDepartamento = new DepartamentoModelo();
                                listarDepartamento.nombreDepartamento = leer.GetString("nombre_departamento");
                                listaDepartamento.Add(listarDepartamento);
                            }
                        }
                    }
                }
                return listaDepartamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los departamentos: {ex.Message}");
                return new List<DepartamentoModelo>();
            }
        }
    }
}
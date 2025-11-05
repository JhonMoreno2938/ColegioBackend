using Colegio.Interfaz;
using Colegio.Modelos.Genero;
using Colegio.Modelos.Genero.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class GeneroDato : IGenero
    {
        private readonly string conexion;
        private static readonly string queryListaGenero = "select nombre_genero from listar_generos";

        public GeneroDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<GeneroModelo>> InformacionGeneroAsync()
        {
            var listaGenero = new List<GeneroModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaGenero, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarGenero = new GeneroModelo();
                                listarGenero.nombreGenero = leer.GetString("nombre_genero");
                                listaGenero.Add(listarGenero);
                            }
                        }
                    }
                }
                return listaGenero;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los generos: {ex.Message}");
                return new List<GeneroModelo>();
            }
        }
    }
}
using Colegio.Interfaz;
using Colegio.Modelos.Rh.Vistas;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class RhDato : IRh
    {
        private readonly string conexion;
        private static readonly string queryListaRh = "select nombre_rh from listar_rh";

        public RhDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<ListarRh>> InformacionRhAsync()
        {
            var listaRh = new List<ListarRh>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaRh, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarRh = new ListarRh();
                                listarRh.nombreRh = leer.GetString("nombre_rh");
                                listaRh.Add(listarRh);
                            }
                        }
                    }
                }
                return listaRh;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los RH: {ex.Message}");
                return new List<ListarRh>();
            }
        }

    }
}

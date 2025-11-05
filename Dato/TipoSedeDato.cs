using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Sede;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class TipoSedeDato : ITipoSede
    {
        private readonly string conexion;
        private static readonly string queryListaTipoSede = "select nombre_tipo_sede from listar_tipo_sedes";

        public TipoSedeDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<TipoSedeModelo>> InformacionTipoSedeAsync()
        {
            var listaTipoSede = new List<TipoSedeModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaTipoSede, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarTipoSede = new TipoSedeModelo();
                                listarTipoSede.nombreTipoSede = leer.GetString("nombre_tipo_sede");
                                listaTipoSede.Add(listarTipoSede);
                            }
                        }
                    }
                }
                return listaTipoSede;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipo de sedes: {ex.Message}");
                return new List<TipoSedeModelo>();
            }
        }
    }
}

using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Documento;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class TipoDocumentoDato : ITipoDocumento
    {
        private readonly string conexion;
        private static readonly string queryListaTipoDocumento = "select nombre_tipo_documento from listar_tipos_documentos";

        public TipoDocumentoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<TipoDocumentoModelo>> InformacionTipoDocumentoAsync()
        {
            var listaTipoDocumento = new List<TipoDocumentoModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaTipoDocumento, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarTipoDocumento = new TipoDocumentoModelo();
                                listarTipoDocumento.nombreTipoDocumento = leer.GetString("nombre_tipo_documento");
                                listaTipoDocumento.Add(listarTipoDocumento);
                            }
                        }
                    }
                }
                return listaTipoDocumento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de documentos: {ex.Message}");
                return new List<TipoDocumentoModelo>();
            }
        }
    }
}

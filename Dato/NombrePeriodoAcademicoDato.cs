using Colegio.Interfaz;
using Colegio.Modelos.Nombre_Periodo_Academico;
using MySql.Data.MySqlClient;
using System.Data;

namespace Colegio.Dato
{
    public class NombrePeriodoAcademicoDato : INombrePeriodoAcademico
    {
        private readonly string conexion;
        private static readonly string queryListaNombrePeriodoAcademico = "select nombre_periodo_academico from listar_nombres_periodos_academicos";

        public NombrePeriodoAcademicoDato(IConfiguration configuracion)
        {
            conexion = configuracion.GetConnectionString("CadenaConexion")
                       ?? throw new ArgumentNullException(nameof(configuracion), "La cadena de conexión no puede ser nula");
        }

        public async Task<List<NombrePeriodoAcademicoModelo>> InformacionPeriodoAcademicoAsync()
        {
            var listaNombrePeriodoAcademico = new List<NombrePeriodoAcademicoModelo>();

            try
            {
                using (var conexion2 = new MySqlConnection(conexion))
                {
                    await conexion2.OpenAsync();

                    using (var comando = new MySqlCommand(queryListaNombrePeriodoAcademico, conexion2))
                    {
                        using (var leer = await comando.ExecuteReaderAsync())
                        {
                            while (await leer.ReadAsync())
                            {
                                var listarNombrePeriodoAcademico = new NombrePeriodoAcademicoModelo();
                                listarNombrePeriodoAcademico.nombrePeriodoAcademico = leer.GetString("nombre_periodo_academico");
                                listaNombrePeriodoAcademico.Add(listarNombrePeriodoAcademico);
                            }
                        }
                    }
                }
                return listaNombrePeriodoAcademico;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los nombre de los periodos academicos: {ex.Message}");
                return new List<NombrePeriodoAcademicoModelo>();
            }
        }
    }
}

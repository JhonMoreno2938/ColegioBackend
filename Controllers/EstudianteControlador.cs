using Colegio.Modelos.Estudiante.Procedimientos;
using Colegio.Servicios;
using Colegio.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Colegio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Estudiante")]
    public class EstudianteControlador : ControllerBase
    {
        private readonly EstudianteServicio estudianteServicio;

        public EstudianteControlador(EstudianteServicio estudianteServicio)
        {
            this.estudianteServicio = estudianteServicio;
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPost("RegistrarEstudiante")]
        public async Task<IActionResult> RegistrarEstudiante([FromBody] RegistrarEstudiante registrarEstudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await estudianteServicio.ValidarInformacionRegistrarEstudianteAsync(registrarEstudiante);

                if (resultado.exitoso)
                {
                    return CreatedAtAction(nameof(RegistrarEstudiante), new { Id = resultado.mensajeId, Mensaje = resultado.mensaje });
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [Consumes("multipart/form-data")]
        [HttpPost("CargarArchivoEstudiantes")]
        public async Task<IActionResult> CargarArchivoEstudiantes([FromForm] ArchivoEstudiante request)
        {
            var archivo = request.Archivo;
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido.");

            var resultadosRegistro = new List<string>();

            try
            {
                using var stream = new MemoryStream();
                await archivo.CopyToAsync(stream);

                ExcelPackage.License.SetNonCommercialPersonal("Alexander Moreno");

                using var package = new ExcelPackage(stream);
                var hoja = package.Workbook.Worksheets[0];
                int filas = hoja.Dimension.Rows;
                int columnas = hoja.Dimension.Columns;

                var mapaColumnas = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int col = 1; col <= columnas; col++)
                {
                    var encabezado = hoja.Cells[1, col].Text.Trim();
                    if (!string.IsNullOrWhiteSpace(encabezado))
                        mapaColumnas[encabezado] = col;
                }

                var requeridas = new[]
                {
            "NOMBRE1", "NOMBRE2", "APELLIDO1", "APELLIDO2", "DOC", "FECHA_NACIMIENTO", "TIPODOC", "GENERO",
            "TIPO DE SANGRE", "NUI", "ESTADO", "EPS", "ESTRATO", "DISCAPACIDAD", "SISBEN IV", "SEDE", "JORNADA",
            "GRADO_COD", "GRUPO", "ANO"
        };

                foreach (var nombre in requeridas)
                {
                    if (!mapaColumnas.ContainsKey(nombre))
                        return BadRequest($"Falta la columna requerida: {nombre}");
                }

                for (int fila = 2; fila <= filas; fila++)
                {
                    try
                    {
                        var estudiante = new CargueEstudianteCSV
                        {
                            primerNombre = hoja.Cells[fila, mapaColumnas["NOMBRE1"]].Text.Trim(),
                            segundoNombre = hoja.Cells[fila, mapaColumnas["NOMBRE2"]].Text.Trim(),
                            primerApellido = hoja.Cells[fila, mapaColumnas["APELLIDO1"]].Text.Trim(),
                            segundoApellido = hoja.Cells[fila, mapaColumnas["APELLIDO2"]].Text.Trim(),
                            numeroDocumento = hoja.Cells[fila, mapaColumnas["DOC"]].Text.Trim(),
                            fechaNacimiento = hoja.Cells[fila, mapaColumnas["FECHA_NACIMIENTO"]].Text.Trim(),
                            edad = 0, // El servicio lo calculará/sobrescribirá
                            nombreTipoDocumento = hoja.Cells[fila, mapaColumnas["TIPODOC"]].Text.Trim(),
                            nombreGenero = hoja.Cells[fila, mapaColumnas["GENERO"]].Text.Trim(),
                            nombreRh = hoja.Cells[fila, mapaColumnas["TIPO DE SANGRE"]].Text.Trim(),
                            codigoEstudiante = hoja.Cells[fila, mapaColumnas["NUI"]].Text.Trim(),
                            estadoEstudiante = hoja.Cells[fila, mapaColumnas["ESTADO"]].Text.Trim(),
                            nombreEps = hoja.Cells[fila, mapaColumnas["EPS"]].Text.Trim(),
                            nombreEstratoSocial = hoja.Cells[fila, mapaColumnas["ESTRATO"]].Text.Trim(),
                            nombreDiscapacidad = hoja.Cells[fila, mapaColumnas["DISCAPACIDAD"]].Text.Trim(),
                            nombreSisben = hoja.Cells[fila, mapaColumnas["SISBEN IV"]].Text.Trim(),
                            nombreSede = hoja.Cells[fila, mapaColumnas["SEDE"]].Text.Trim(),
                            nombreJornada = hoja.Cells[fila, mapaColumnas["JORNADA"]].Text.Trim(),
                            nombreGrado = hoja.Cells[fila, mapaColumnas["GRADO_COD"]].Text.Trim(),
                            nombreGrupo = hoja.Cells[fila, mapaColumnas["GRUPO"]].Text.Trim(),
                            annioActual = Convert.ToInt32(hoja.Cells[fila, mapaColumnas["ANO"]].Text.Trim()),
                        };

                        var resultadoRegistro = await estudianteServicio.ValidarCargueEstudianteCSV(estudiante);

                        if (resultadoRegistro.exitoso)
                        {
                            resultadosRegistro.Add($"✅ Fila {fila}: {resultadoRegistro.mensaje}");
                        }
                        else
                        {
                            resultadosRegistro.Add($"❌ Fila {fila}: ERROR - {resultadoRegistro.mensaje}");
                        }
                    }
                    catch (Exception exFila)
                    {
                        string errorMsg = $"Error crítico en fila {fila}: {exFila.Message}";
                        resultadosRegistro.Add($"❌ {errorMsg}");
                    }
                }

                if (resultadosRegistro.Any(r => r.StartsWith("❌")))
                {
                    return Ok(new { Mensaje = "Carga de archivo finalizada. Se encontraron errores en algunas filas.", Detalles = resultadosRegistro });
                }

                return Ok(new { Mensaje = "Archivo procesado y todos los estudiantes registrados correctamente.", Detalles = resultadosRegistro });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error general al procesar archivo: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpPut("ModificarInformacionEstudiante")]
        public async Task<IActionResult> ModificarInformacionEstudiante([FromBody] ModificarInformacionEstudiante modificarInformacionEstudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultado = await estudianteServicio.ValidarModificarInformacionEstudianteAsync(modificarInformacionEstudiante);

                if (resultado.exitoso)
                {
                    // Se usa 200 OK para la modificación exitosa.
                    return Ok(new { Mensaje = resultado.mensaje });
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }


        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("{numeroDocumento}")]
        public async Task<IActionResult> ConsultarEstudiante([FromRoute] string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                return BadRequest(new { Mensaje = "El número de documento es requerido para la consulta." });
            }

            try
            {
                var resultado = await estudianteServicio.ValidarConsutlarEstudianteAsync(numeroDocumento);

                if (resultado.exito)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            // El manejo de ApplicationException ya es correcto, mantenemos el catch de Exception para errores generales.
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("InformacionEstudiante")]
        public async Task<IActionResult> InformacionEstudiante()
        {
            try
            {
                var listaEstudiante = await estudianteServicio.ValidarInformacionEstudianteAsync();
                return Ok(listaEstudiante);
            }
            catch (Exception ex)
            {
                // Se devuelve 500 Internal Server Error para fallas internas/de servidor.
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [Authorize(Policy = "SoloSecretario")]
        [HttpGet("ProcesarCargueEstudianteAsync")]
        public async Task<IActionResult> ProcesarCargueEstudianteAsync()
        {
            try
            {
                var validarProcesoCargueEstudiante = await estudianteServicio.ValidarProcesarCargueEstudianteAsync();
                return Ok(validarProcesoCargueEstudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error interno del servidor: {ex.Message}" });
            }
        }


    }

}
using Colegio.Interfaz;
using Colegio.Modelos.Persona;
using Colegio.Modelos.Estudiante;
using Colegio.Modelos.Estudiante.Procedimientos;
using Colegio.Modelos.Tipo_Documento;
using Colegio.Modelos.Genero;
using Colegio.Modelos.Departamento;
using Colegio.Modelos.Ciudad;
using Colegio.Modelos.Rh;
using Colegio.Modelos.Eps;
using Colegio.Modelos.Estrato_Social;
using Colegio.Modelos.Discapcidad;
using Colegio.Modelos.Sisben;
using Colegio.Utilidades;
using System.Globalization;
using Colegio.Modelos.Estudiante.Consultas;
using Colegio.Modelos.Estudiante.Vistas;

namespace Colegio.Servicios
{
    public class EstudianteServicio
    {
        private readonly IEstudiante estudiante;

        public EstudianteServicio(IEstudiante estudiante)
        {
            this.estudiante = estudiante;
        }

        private EstudianteModelo MapearRegistrarEstudiante(RegistrarEstudiante registrarEstudiante)
        {
            var tipoDocumento = new TipoDocumentoModelo()
            {
                nombreTipoDocumento = registrarEstudiante.nombreTipoDocumento
            };

            var genero = new GeneroModelo()
            {
                nombreGenero = registrarEstudiante.nombreGenero
            };

            var departamentoNacimiento = new DepartamentoModelo()
            {
                nombreDepartamento = registrarEstudiante.nombreDepartamentoNacimiento
            };

            var departamentoExpedicion = new DepartamentoModelo()
            {
                nombreDepartamento = registrarEstudiante.nombreDepartamentoExpedicionDocumento
            };

            var ciudadNacimiento = new CiudadModelo()
            {
                nombreCiudad = registrarEstudiante.nombreCiudadNacimiento
            };

            var ciudadExpedicion = new CiudadModelo()
            {
                nombreCiudad = registrarEstudiante.nombreCiudadExpedicionDocumento
            };

            var rh = new RhModelo()
            {
                nombreRh = registrarEstudiante.nombreRh
            };

            var eps = new EpsModelo()
            {
                nombreEps = registrarEstudiante.nombreEps
            };

            var estratoSocial = new EstratoSocialModelo()
            {
                nombreEstratoSocial = registrarEstudiante.nombreEstratoSocial
            };

            var discapacidad = new DiscapacidadModelo()
            {
                nombreDiscapacidad = registrarEstudiante.nombreDiscapacidad
            };

            var sisben = new SisbenModelo()
            {
                nombreSisben = registrarEstudiante.nombreSisben
            };


            var persona = new PersonaModelo
            {
                primerNombrePersona = registrarEstudiante.primerNombre,
                segundoNombrePersona = registrarEstudiante.segundoNombre,
                primerApellidoPersona = registrarEstudiante.primerApellido,
                segundoApellidoPersona = registrarEstudiante.segundoApellido,
                numeroDocumentoPersona = registrarEstudiante.numeroDocumento,
                fechaNacimientoPersona = registrarEstudiante.fechaNacimiento,
                edadPersona = registrarEstudiante.edad,

                tipoDocumentoModelo = tipoDocumento,
                generoModelo = genero,

                departamentoNacimientoModelo = departamentoNacimiento,
                departamentoExpedicionDocumentoModelo = departamentoExpedicion,
                ciudadNacimientoModelo = ciudadNacimiento,
                ciudadExpedicionModelo = ciudadExpedicion,
                rhModelo = rh,

            };


            return new EstudianteModelo
            {
                personaModelo = persona,
                epsModelo = eps,
                estratoSocialModelo = estratoSocial,
                discapacidadModelo = discapacidad,
                sisbenModelo = sisben,

            };
        }


        private EstudianteModelo MapearCargueEstudiante(CargueEstudianteCSV cargueEstudianteCSV)
        {

            var tipoDocumento = new TipoDocumentoModelo()
            {
                nombreTipoDocumento = cargueEstudianteCSV.nombreTipoDocumento
            };

            var genero = new GeneroModelo()
            {
                nombreGenero = cargueEstudianteCSV.nombreGenero
            };

            var rh = new RhModelo()
            {
                nombreRh = cargueEstudianteCSV.nombreRh
            };

            var eps = new EpsModelo()
            {
                nombreEps = cargueEstudianteCSV.nombreEps
            };

            var estratoSocial = new EstratoSocialModelo()
            {
                nombreEstratoSocial = cargueEstudianteCSV.nombreEstratoSocial
            };

            var discapacidad = new DiscapacidadModelo()
            {
                nombreDiscapacidad = cargueEstudianteCSV.nombreDiscapacidad
            };

            var sisben = new SisbenModelo()
            {
                nombreSisben = cargueEstudianteCSV.nombreSisben
            };


            var persona = new PersonaModelo
            {
                primerNombrePersona = cargueEstudianteCSV.primerNombre,
                segundoNombrePersona = cargueEstudianteCSV.segundoNombre,
                primerApellidoPersona = cargueEstudianteCSV.primerApellido,
                segundoApellidoPersona = cargueEstudianteCSV.segundoApellido,
                numeroDocumentoPersona = cargueEstudianteCSV.numeroDocumento,
                fechaNacimientoPersona = cargueEstudianteCSV.fechaNacimiento,
                edadPersona = cargueEstudianteCSV.edad,

                tipoDocumentoModelo = tipoDocumento,
                generoModelo = genero,

                rhModelo = rh,

            };


            return new EstudianteModelo
            {
                personaModelo = persona,
                epsModelo = eps,
                estratoSocialModelo = estratoSocial,
                discapacidadModelo = discapacidad,
                sisbenModelo = sisben,

                codigoEstudiante = cargueEstudianteCSV.codigoEstudiante,
                estadoEstudiante = cargueEstudianteCSV.estadoEstudiante

            };
        }

        private EstudianteModelo MapearMdificarInformacionEstudiante(ModificarInformacionEstudiante  modificarInformacionEstudiante)
        {
            var tipoDocumento = new TipoDocumentoModelo()
            {
                nombreTipoDocumento = modificarInformacionEstudiante.nombreTipoDocumento
            };

            var departamentoNacimiento = new DepartamentoModelo()
            {
                nombreDepartamento = modificarInformacionEstudiante.nombreDepartamentoNacimiento
            };

            var departamentoExpedicion = new DepartamentoModelo()
            {
                nombreDepartamento = modificarInformacionEstudiante.nombreDepartamentoExpedicionDocumento
            };

            var ciudadNacimiento = new CiudadModelo()
            {
                nombreCiudad = modificarInformacionEstudiante.nombreCiudadNacimiento
            };

            var ciudadExpedicion = new CiudadModelo()
            {
                nombreCiudad = modificarInformacionEstudiante.nombreCiudadExpedicionDocumento
            };

            var eps = new EpsModelo()
            {
                nombreEps = modificarInformacionEstudiante.nombreEps
            };

            var persona = new PersonaModelo
            {
                primerNombrePersona = modificarInformacionEstudiante.primerNombre,
                segundoNombrePersona = modificarInformacionEstudiante.segundoNombre,
                primerApellidoPersona = modificarInformacionEstudiante.primerApellido,
                segundoApellidoPersona = modificarInformacionEstudiante.segundoApellido,
                numeroDocumentoPersona = modificarInformacionEstudiante.numeroDocumento,

                tipoDocumentoModelo = tipoDocumento,
              
                departamentoNacimientoModelo = departamentoNacimiento,
                departamentoExpedicionDocumentoModelo = departamentoExpedicion,
                ciudadNacimientoModelo = ciudadNacimiento,
                ciudadExpedicionModelo = ciudadExpedicion,

            };


            return new EstudianteModelo
            {
                personaModelo = persona,
                epsModelo = eps,

            };
        }

        public async Task<ResultadoConID> ValidarInformacionRegistrarEstudianteAsync(RegistrarEstudiante registrarEstudiante)
        {
            EstudianteModelo estudianteModelo = MapearRegistrarEstudiante(registrarEstudiante);

            ResultadoConID resultado = await estudiante.RegistrarEstudianteAsync(
                estudianteModelo,
                registrarEstudiante.nombreSede,
                registrarEstudiante.nombreJornada,
                registrarEstudiante.nombreGrado,
                registrarEstudiante.nombreGrupo
                );

            return resultado;
        }
        public async Task<ResultadoOperacion> ValidarCargueEstudianteCSV(CargueEstudianteCSV cargueEstudianteCSV)
        {
            string nombreTipoDocumento = string.Empty;

            if (!string.IsNullOrEmpty(cargueEstudianteCSV.nombreTipoDocumento))
            {
                var partes = cargueEstudianteCSV.nombreTipoDocumento.Split(':');
                nombreTipoDocumento = partes.Length > 0 ? partes.Last().Trim() : string.Empty;
            }

            string[] formatosPermitidos = new[] {
        "d/M/yyyy",
        "dd/MM/yyyy",
        "d/MM/yyyy",
        "dd/M/yyyy"
    };

            DateTime fechaNacimientoDT;
            string fechaNacimientoMySQL = string.Empty;

            if (DateTime.TryParseExact(
                    cargueEstudianteCSV.fechaNacimiento.Trim(),
                    formatosPermitidos,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaNacimientoDT
                ))
            {
                fechaNacimientoMySQL = fechaNacimientoDT.ToString("yyyy-MM-dd");
            }
            else
            {
                return new ResultadoOperacion
                {
                    exitoso = false,
                    mensaje = $"Error de formato en la Fecha de Nacimiento: Se esperaba uno de los siguientes formatos (D/M/AAAA o DD/MM/AAAA)."
                };
            }


            EstudianteModelo estudianteModelo = MapearCargueEstudiante(cargueEstudianteCSV);

            DateTime fechaReferencia = DateTime.Today;

            int edadCalculada = fechaReferencia.Year - fechaNacimientoDT.Year;

            if (fechaNacimientoDT.Date > fechaReferencia.AddYears(-edadCalculada))
            {
                edadCalculada--;
            }

            estudianteModelo.personaModelo.fechaNacimientoPersona = fechaNacimientoMySQL;
            estudianteModelo.personaModelo.edadPersona = edadCalculada;

            ResultadoOperacion resultado = await estudiante.CargueEstudianteCSVAsync(
                estudianteModelo,
                nombreTipoDocumento,
                cargueEstudianteCSV.nombreSede,
                cargueEstudianteCSV.nombreJornada,
                cargueEstudianteCSV.nombreGrado,
                cargueEstudianteCSV.nombreGrupo,
                cargueEstudianteCSV.annioActual
                );

            return resultado;
        }

        public async Task<ResultadoOperacion> ValidarModificarInformacionEstudianteAsync(ModificarInformacionEstudiante modificarInformacionEstudiante)
        {
            EstudianteModelo estudianteModelo = MapearMdificarInformacionEstudiante(modificarInformacionEstudiante);

            ResultadoOperacion resultado = await estudiante.ModificarInformacionEstudianteAsync(estudianteModelo);

            return resultado;
        }

        public async Task<ConsultarEstudiante> ValidarConsutlarEstudianteAsync(string numeroDocumento)
        {
            ConsultarEstudiante consultarEstudiante = await estudiante.ConsultarEstudianteAsync(numeroDocumento);

            return consultarEstudiante;
        }

        public async Task<List<ListarEstudiante>> ValidarInformacionEstudianteAsync()
        {
            List<ListarEstudiante> listarEstudiante = await estudiante.InformacionEstudianteAsync();

            return listarEstudiante;
        }

        public async Task<ResultadoOperacion> ValidarProcesarCargueEstudianteAsync()
        {
            ResultadoOperacion resultadoOperacion = await estudiante.ProcesarCargueEstudianteAsync();

            return resultadoOperacion;
        }
    }
}

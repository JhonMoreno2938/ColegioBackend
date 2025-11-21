using Colegio.Interfaz;
using Colegio.Modelos.Funcionario;
using Colegio.Modelos.Funcionario.Consultas;
using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using Colegio.Modelos.Funcionario.Vistas;
using Colegio.Modelos.Persona;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class FuncionarioServicio
    {
        private readonly IFuncionario funcionario;

        public FuncionarioServicio(IFuncionario funcionario)
        {
            this.funcionario = funcionario;
        }

        private FuncionarioModelo MapearRegistrarFuncionario(RegistrarFuncionario registrarFuncionario)
        {
        
            var tipoDocumento = new Colegio.Modelos.Tipo_Documento.TipoDocumentoModelo()
            {
                pkIdTipoDocumento = 0, 
                nombreTipoDocumento = registrarFuncionario.nombreTipoDocumento
            };

            var genero = new Colegio.Modelos.Genero.GeneroModelo()
            {
                pkIdGenero = 0,
                nombreGenero = registrarFuncionario.nombreGenero
            };

            var tipoFuncionario = new Colegio.Modelos.Tipo_Funcionario.TipoFuncionarioModelo
            {
                pkIdTipoFuncionario = 0,
                nombreTipoFuncionario = registrarFuncionario.nombreTipoFuncionario
            };

            var persona = new PersonaModelo
            {
                primerNombrePersona = registrarFuncionario.primerNombre,
                segundoNombrePersona = registrarFuncionario.segundoNombre,
                primerApellidoPersona = registrarFuncionario.primerApellido,
                segundoApellidoPersona = registrarFuncionario.segundoApellido,
                numeroDocumentoPersona = registrarFuncionario.numeroDocumento,

                tipoDocumentoModelo = tipoDocumento,
                generoModelo = genero,


            };

            var usuario = new Colegio.Modelos.Usuario.UsuarioModelo()
            {
                pkIdUsuario = 0,
            };

            return new FuncionarioModelo
            {
                personaModelo = persona,
                tipoFuncionarioModelo = tipoFuncionario,
                usuarioModelo = usuario,

                pkIdFuncionario = 0,
                intensidadHorariaFuncionario = 0
            };
        }

        private FuncionarioModelo MapearModificarInformacionFuncionario(ModificarInformacionFuncionario modificarInformacionFuncionario)
        {

            var persona = new PersonaModelo
            {
                primerNombrePersona = modificarInformacionFuncionario.primerNombre,
                segundoNombrePersona = modificarInformacionFuncionario.segundoNombre,
                primerApellidoPersona = modificarInformacionFuncionario.primerApellido,
                segundoApellidoPersona = modificarInformacionFuncionario.segundoApellido,
                numeroDocumentoPersona = modificarInformacionFuncionario.numeroDocumento,

            };

           
            return new FuncionarioModelo
            {
                personaModelo = persona,

                pkIdFuncionario = 0,
                intensidadHorariaFuncionario = 0
            };
        }

        public async Task<SalidaRegistrarFuncionario> ValidarInformacionRegistrarFuncionarioAsync(RegistrarFuncionario registrarFuncionario)
        {
            FuncionarioModelo funcionarioModelo = MapearRegistrarFuncionario(registrarFuncionario);

            SalidaRegistrarFuncionario resultado = await funcionario.RegistrarFuncionarioAsync(funcionarioModelo);

            return resultado;
        }

        public async Task<ResultadoOperacion> ValidarModificarInformacionFuncionarioAsync(ModificarInformacionFuncionario modificarInformacionFuncionario)
        {
            FuncionarioModelo funcionarioModelo = MapearModificarInformacionFuncionario(modificarInformacionFuncionario);

            ResultadoOperacion resultadoOperacion = await funcionario.ModificarInformacionFuncionarioAsync(funcionarioModelo);

            return resultadoOperacion;
        }

        public async Task<ConsultarFuncionario> ValidarConsultarFuncionarioAsync(string numeroDocumento)
        {
            ConsultarFuncionario consultarFuncionario = await funcionario.ConsultarFuncionarioAsync(numeroDocumento);

            return consultarFuncionario;
        }

        public async Task<List<ListarFuncionario>> ValidarInformacionFuncionarioAsync()
        {
            List<ListarFuncionario> listarFuncionario = await funcionario.InformacionFuncionarioAsync();

            return listarFuncionario;
        }

        public async Task<List<ConsultarGradoGrupoFuncionarioEstadoActivo>> ValidarInformacionGradoGrupoFuncionarioEstadoActivoAsync(string nombreUsuario)
        {
            List <ConsultarGradoGrupoFuncionarioEstadoActivo> consultarGradoGrupoFuncionarioEstadoActivo = await funcionario.InformacionGradoGrupoFuncionarioEstadoActivoAsync(nombreUsuario);

            return consultarGradoGrupoFuncionarioEstadoActivo;
        }

        public async Task<List<ConsultarCompetenciaFuncionario>> ValidarConsultarCompetenciaFuncionarioAsync(string nombreUsuario)
        {
            List<ConsultarCompetenciaFuncionario> mostrarCompetenciaFuncionario = await funcionario.ConsultarCompetenciaFuncionarioAsync(nombreUsuario);

            return mostrarCompetenciaFuncionario;
        }

    }
}

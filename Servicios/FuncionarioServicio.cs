using Colegio.Interfaz;
using Colegio.Modelos.Funcionario;
using Colegio.Modelos.Funcionario.Procedimientos;
using Colegio.Modelos.Funcionario.Salidas_Procedimientos;
using Colegio.Modelos.Persona;

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

                departamentoNacimientoModelo = new Colegio.Modelos.Departamento.DepartamentoModelo(),
                departamentoExpedicionDocumento = new Colegio.Modelos.Departamento.DepartamentoModelo(),
                ciudadNacimientoModelo = new Colegio.Modelos.Ciudad.CiudadModelo(),
                ciudadExpedicionModelo = new Colegio.Modelos.Ciudad.CiudadModelo(),
                rhModelo = new Colegio.Modelos.Rh.RhModelo(),

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

        public async Task<SalidaRegistrarFuncionario> ValidarInformacionRegistrarFuncionarioAsync(RegistrarFuncionario registrarFuncionario)
        {
            FuncionarioModelo funcionarioModelo = MapearRegistrarFuncionario(registrarFuncionario);

            SalidaRegistrarFuncionario resultado = await funcionario.RegistrarFuncionarioAsync(funcionarioModelo);

            return resultado;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Funcionario.Procedimientos
{
    public class RegistrarFuncionario
    {
        [JsonIgnore]
        private readonly FuncionarioModelo funcionarioModelo = new FuncionarioModelo();

        [JsonPropertyName("primerNombre")]
        [StringLength(100)]
        [Required]
        public string primerNombre
        {
            get => funcionarioModelo.personaModelo.primerNombrePersona;
            set => funcionarioModelo.personaModelo.primerNombrePersona = value;
        }

        [JsonPropertyName("segundoNombre")]
        [StringLength(100)]
        public string? segundoNombre
        {
            get => funcionarioModelo.personaModelo.segundoNombrePersona;
            set => funcionarioModelo.personaModelo.segundoNombrePersona = value;
        }

        [JsonPropertyName("primerApellido")]
        [StringLength(100)]
        [Required]
        public string primerApellido
        {
            get => funcionarioModelo.personaModelo.primerApellidoPersona;
            set => funcionarioModelo.personaModelo.primerApellidoPersona = value;
        }

        [JsonPropertyName("segundoApellido")]
        [StringLength(100)]
        public string? segundoApellido
        {
            get => funcionarioModelo.personaModelo.segundoApellidoPersona;
            set => funcionarioModelo.personaModelo.segundoApellidoPersona = value;
        }

        [JsonPropertyName("numeroDocumento")]
        [StringLength(10)]
        [Required]
        public string numeroDocumento
        {
            get => funcionarioModelo.personaModelo.numeroDocumentoPersona;
            set => funcionarioModelo.personaModelo.numeroDocumentoPersona = value;
        }

        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        [Required]
        public string nombreTipoDocumento
        {
            get => funcionarioModelo.personaModelo.tipoDocumentoModelo.nombreTipoDocumento;
            set => funcionarioModelo.personaModelo.tipoDocumentoModelo.nombreTipoDocumento = value;
        }

        [JsonPropertyName("nombreGenero")]
        [StringLength(10)]
        [Required]
        public string nombreGenero
        {
            get => funcionarioModelo.personaModelo.generoModelo.nombreGenero;
            set => funcionarioModelo.personaModelo.generoModelo.nombreGenero = value;
        }

        [JsonPropertyName("nombreTipoFuncionario")]
        [StringLength(20)]
        [Required]
        public string nombreTipoFuncionario
        {
            get => funcionarioModelo.tipoFuncionarioModelo.nombreTipoFuncionario;
            set => funcionarioModelo.tipoFuncionarioModelo.nombreTipoFuncionario = value;
        }

    }
}

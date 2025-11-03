using Colegio.Modelos.Ciudad;
using Colegio.Modelos.Departamento;
using Colegio.Modelos.Genero;
using Colegio.Modelos.Rh;
using Colegio.Modelos.Tipo_Documento;
using Org.BouncyCastle.Tls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Persona
{
    [Table("persona")]
    public class PersonaModelo
    {
        public TipoDocumentoModelo tipoDocumentoModelo = new TipoDocumentoModelo();
        public GeneroModelo generoModelo = new GeneroModelo();
        public DepartamentoModelo departamentoNacimientoModelo = new DepartamentoModelo();
        public DepartamentoModelo departamentoExpedicionDocumento = new DepartamentoModelo();
        public CiudadModelo ciudadNacimientoModelo = new CiudadModelo();
        public CiudadModelo ciudadExpedicionModelo = new CiudadModelo();
        public RhModelo rhModelo = new RhModelo();

        [Key]
        [Column("pk_id_persona")]
        public int pkIdPersona { get; set; } = 0;

        [Column("primer_nombre_persona")]
        public string primerNombrePersona { get; set; } = string.Empty;

        [Column("segundo_nombre_persona")]
        public string? segundoNombrePersona { get; set; } = string.Empty;

        [Column("primer_apellido_persona")]
        public string primerApellidoPersona { get; set; } = string.Empty;

        [Column("segundo_apellido_persona")]
        public string? segundoApellidoPersona { get; set; } = string.Empty;

        [Column("numero_documento_persona")]
        public string numeroDocumentoPersona { get; set; } = string.Empty;

        [Column("fecha_nacimiento_persona")]
        public string? fechaNacimientoPersona { get; set; } = string.Empty;

        [Column("edad_persona")]
        public int? edadPersona  { get; set;} = 0;

        [Column("fk_id_tipo_documento")]
        public int? fkIdTipoDocumento
        {
            get => tipoDocumentoModelo.pkIdTipoDocumento;
            set => tipoDocumentoModelo.pkIdTipoDocumento = 0;
        }

        [Column("fk_id_genero")]
        public int? fkIdGenero
        {
            get => generoModelo.pkIdGenero;
            set => generoModelo.pkIdGenero = 0;
        }

        [Column("fk_id_departamento_nacimiento")]
        public int? fkIdDepartamentoNacimiento
        {
            get => departamentoNacimientoModelo.pkIdDepartamento;
            set => departamentoNacimientoModelo.pkIdDepartamento = 0;
        }

        [Column("fk_id_departamento_expedicion_documento")]
        public int? fkIdDepartamentoExpedicionDocumento
        {
            get => departamentoExpedicionDocumento.pkIdDepartamento;
            set => departamentoExpedicionDocumento.pkIdDepartamento = 0;
        }

        [Column("fk_id_ciudad_nacimiento")]
        public int? fkIdCiudadNacimiento
        {
            get => ciudadNacimientoModelo.pkIdCiudad;
            set => ciudadNacimientoModelo.pkIdCiudad = 0;
        }

        [Column("fk_id_ciudad_expedicion_documento")]
        public int? fkIdCiudadExpedicionDocumento
        {
            get => ciudadExpedicionModelo.pkIdCiudad;
            set => ciudadExpedicionModelo.pkIdCiudad = 0;
        }

        [Column("fk_id_rh")]
        public int? fkIdRh
        {
            get => rhModelo.pkIdRh;
            set => rhModelo.pkIdRh = 0;
        }

        public PersonaModelo()
        {

        }

    }
}

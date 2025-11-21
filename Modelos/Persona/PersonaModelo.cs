using Colegio.Modelos.Ciudad;
using Colegio.Modelos.Departamento;
using Colegio.Modelos.Genero;
using Colegio.Modelos.Rh;
using Colegio.Modelos.Tipo_Documento;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Persona
{
    [Table("persona")]
    public class PersonaModelo
    {
        public TipoDocumentoModelo? tipoDocumentoModelo { get; set; } = new TipoDocumentoModelo();
        public GeneroModelo? generoModelo { get; set; } = new GeneroModelo();
        public DepartamentoModelo? departamentoNacimientoModelo { get; set; } = new DepartamentoModelo();
        public DepartamentoModelo? departamentoExpedicionDocumentoModelo { get; set; } = new DepartamentoModelo();
        public CiudadModelo? ciudadNacimientoModelo { get; set; } = new CiudadModelo();
        public CiudadModelo? ciudadExpedicionModelo { get; set; } = new CiudadModelo();
        public RhModelo? rhModelo { get; set; } = new RhModelo();

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
        public int? edadPersona { get; set; } = 0;

        [Column("fk_id_tipo_documento")]
        public int? fkIdTipoDocumento
        {
            get => tipoDocumentoModelo?.pkIdTipoDocumento; 
            set
            {
                if (tipoDocumentoModelo != null && value.HasValue)
                    tipoDocumentoModelo.pkIdTipoDocumento = value.Value;
            }
        }

        [Column("fk_id_genero")]
        public int? fkIdGenero
        {
            get => generoModelo?.pkIdGenero;
            set
            {
                if (generoModelo != null && value.HasValue)
                    generoModelo.pkIdGenero = value.Value;
            }
        }

        [Column("fk_id_departamento_nacimiento")]
        public int? fkIdDepartamentoNacimiento
        {
            get => departamentoNacimientoModelo?.pkIdDepartamento;
            set
            {
                if (departamentoNacimientoModelo != null && value.HasValue)
                    departamentoNacimientoModelo.pkIdDepartamento = value.Value;
            }
        }

        [Column("fk_id_departamento_expedicion_documento")]
        public int? fkIdDepartamentoExpedicionDocumento
        {
            get => departamentoExpedicionDocumentoModelo?.pkIdDepartamento;
            set
            {
                if (departamentoExpedicionDocumentoModelo != null && value.HasValue)
                    departamentoExpedicionDocumentoModelo.pkIdDepartamento = value.Value;
            }
        }

        [Column("fk_id_ciudad_nacimiento")]
        public int? fkIdCiudadNacimiento
        {
            get => ciudadNacimientoModelo?.pkIdCiudad;
            set
            {
                if (ciudadNacimientoModelo != null && value.HasValue)
                    ciudadNacimientoModelo.pkIdCiudad = value.Value;
            }
        }

        [Column("fk_id_ciudad_expedicion_documento")]
        public int? fkIdCiudadExpedicionDocumento
        {
            get => ciudadExpedicionModelo?.pkIdCiudad;
            set
            {
                if (ciudadExpedicionModelo != null && value.HasValue)
                    ciudadExpedicionModelo.pkIdCiudad = value.Value;
            }
        }

        [Column("fk_id_rh")]
        public int? fkIdRh
        {
            get => rhModelo?.pkIdRh;
            set
            {
                if (rhModelo != null && value.HasValue)
                    rhModelo.pkIdRh = value.Value;
            }
        }

        public PersonaModelo()
        {

        }
    }
}
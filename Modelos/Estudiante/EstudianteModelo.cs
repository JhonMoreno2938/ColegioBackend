using Colegio.Modelos.Discapcidad;
using Colegio.Modelos.Eps;
using Colegio.Modelos.Estrato_Social;
using Colegio.Modelos.Persona;
using Colegio.Modelos.Sisben;
using Colegio.Modelos.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Estudiante
{
    [Table("estudiante")]
    public class EstudianteModelo
    {
        public PersonaModelo personaModelo { get; set; } = new PersonaModelo();
        public EpsModelo epsModelo { get; set; } = new EpsModelo();
        public EstratoSocialModelo estratoSocialModelo { get; set; } = new EstratoSocialModelo();
        public DiscapacidadModelo discapacidadModelo { get; set; } = new DiscapacidadModelo();
        public SisbenModelo sisbenModelo { get; set; } = new SisbenModelo();
        public UsuarioModelo usuarioModelo { get; set; } = new UsuarioModelo();

        [Key]
        [Column("pk_id_estudiante")]
        public int pkIdEstudiante { get; set; } = 0;

        [Column("codigo_estudiante")]
        public string? codigoEstudiante { get; set; } = string.Empty;

        [Column("foto_estudiante")]
        public string? fotoEstudiante { get; set; } = string.Empty;

        [Column("estado_estudiante")]
        public string estadoEstudiante { get; set; } = string.Empty;

        [Column("fk_id_persona")]
        public int fkIdPersona
        {
            get => personaModelo.pkIdPersona;
            set => personaModelo.pkIdPersona = value;
        }

        [Column("fk_id_EPS")]
        public int fkIdEps
        {
            get => epsModelo.pkIdEps;
            set => epsModelo.pkIdEps = value;
        }

        [Column("fk_id_estrato_social")]
        public int fkIdEstratoSocial
        {
            get => estratoSocialModelo.pkIdEstratoSocial;
            set => estratoSocialModelo.pkIdEstratoSocial = value;
        }

        [Column("fk_id_discapacidad")]
        public int fkIdDiscapacidad
        {
            get => discapacidadModelo.pkIdDiscapacidad;
            set => discapacidadModelo.pkIdDiscapacidad = value;
        }

        [Column("fk_id_sisben")]
        public int fkIdSisben
        {
            get => sisbenModelo.pkIdSisben;
            set => sisbenModelo.pkIdSisben = value;
        }

        [Column("fk_id_usuario")]
        public int fkIdUsuario
        {
            get => usuarioModelo.pkIdUsuario;
            set => usuarioModelo.pkIdUsuario = value;
        }
    }
}

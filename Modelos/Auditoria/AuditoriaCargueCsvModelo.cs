using Colegio.Modelos.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Auditoria
{
    [Table("auditoria_cargue_csv")]
    public class AuditoriaCargueCsvModelo
    {
        public UsuarioModelo usuarioModelo { get; set; } = new UsuarioModelo();

        [Key]
        [Column("pk_id_auditoria_cargue_csv")]
        public int pkIdAuditoriaCargueCSV { get; set; } = 0;

        [Column("fecha_cargue")]
        public string fechaCargue { get; set; } = string.Empty;

        [Column("estado_auditoria_cargue_csv")]
        public string estadoAuditoriaCargueCsv { get; set; } = string.Empty;

        [Column("fk_id_secretario")]
        public int fkIdSecretario
        {
            get => usuarioModelo.pkIdUsuario;
            set => usuarioModelo.pkIdUsuario = value;
        }
    }
}

using Colegio.Modelos.Grado_Grupo;
using Colegio.Modelos.Jornada_Sede;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Sede_Jornada_Grado_Grupo
{
    [Table("sede_jornada_grado_grupo")]
    public class SedeJornadaGradoGrupoModelo
    {
        public JornadaSedeModelo jornadaSedeModelo = new JornadaSedeModelo();
        public GradoGrupoModelo gradoGrupoModelo = new GradoGrupoModelo();

        [Key]
        [Column("pk_id_sede_jornada_grado_grupo")]
        public int pkIdSedeJornadaGradoGrupo { get; set; } = 0;

        [Column("estado_sede_jornada_grado_grupo")]
        public string estadoSedeJornadaGradoGrupo { get; set; } = string.Empty;

        [Column("fk_id_grado_grupo")]
        public int fkIdGradoGrupo
        {
            get => gradoGrupoModelo.pkIdGradoGrupo;
            set => gradoGrupoModelo.pkIdGradoGrupo = value;
        }

        [Column("fk_id_jornada_sede")]
        public int fkIdJornadaSede
        {
            get => jornadaSedeModelo.pkIdJornadaSede;
            set => jornadaSedeModelo.pkIdJornadaSede = value;
        }
    }
}

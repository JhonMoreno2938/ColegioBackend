using Colegio.Modelos.Grado;
using Colegio.Modelos.Grupo;
using Colegio.Modelos.Nivel_Escolaridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Grado_Grupo
{
    [Table("grado_grupo")]
    public class GradoGrupoModelo
    {

        public GradoModelo gradoModelo = new GradoModelo();
        public GrupoModelo grupoModelo = new GrupoModelo();
        public NivelEscolaridadModelo nivelEscolaridadModelo = new NivelEscolaridadModelo();

        [Key]
        [Column("pk_id_grado_grupo")]
        public int pkIdGradoGrupo { get; set; }

        [Column("estado_grado_grupo")]
        public string estadoGradoGrupo { get; set; }

        [Column("fk_id_grado")]
        public int fkIdGrado
        {
            get => gradoModelo.pkIdGrado;
            set => gradoModelo.pkIdGrado = value;
        }

        [Column("fk_id_grupo")]
        public int fkIdGrupo
        {
            get => grupoModelo.pkIdGrupo;
            set => grupoModelo.pkIdGrupo = value;
        }

        [Column("fk_id_nivel_escolaridad")]
        public int fk_id_nivel_escolaridad
        {
            get => nivelEscolaridadModelo.pkIdNivelEscolaridad;
            set => nivelEscolaridadModelo.pkIdNivelEscolaridad = value;
        }


    }
}

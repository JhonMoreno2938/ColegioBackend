using Colegio.Modelos.Jornada;
using Colegio.Modelos.Sede;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Jornada_Sede
{
    [Table("jornada_sede")]
    public class JornadaSedeModelo
    {

        public SedeModelo sedeModelo = new SedeModelo();
        public JornadaModelo jornadaModelo = new JornadaModelo();

        [Key]
        [Column("pk_id_jornada_sede")]
        public int pkIdJornadaSede { get; set; } = 0;

        [Column("estado_jornada_sede")]
        public string estadoJornadaSede { get; set; } = string.Empty;

        [Column("fk_id_sede")]
        public int fkIdSede
        {
            get => sedeModelo.pkIdSede;
            set => sedeModelo.pkIdSede = value;
        }

        [Column("fk_id_jornada")]
        public int fkIdJornada
        {
            get => jornadaModelo.pkIdJornada;
            set => jornadaModelo.pkIdJornada = value;
        }
    }
}

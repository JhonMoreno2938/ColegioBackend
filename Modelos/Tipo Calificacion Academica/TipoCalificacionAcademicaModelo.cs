using Colegio.Modelos.Porcentaje;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Tipo_Calificacion_Academica
{
    [Table("tipo_calificacion_academica")]
    public class TipoCalificacionAcademicaModelo
    {
        public PorcentajeModelo porcentajeModelo { get; set; } = new PorcentajeModelo();

        [Key]
        [Column("pk_id_tipo_calificacion_academica")]
        public int pkIdTipoCalificacionAcademica { get; set; } = 0;

        [Column("nombre_tipo_calificacion_academica")]
        public string nombreTipoCalificacionAcademica { get; set; } = string.Empty;

        [Column("estado_tipo_calificacion_academica")]
        public string estadoTipoCalificacionAcademica { get; set; } = string.Empty;

        [Column("fk_id_porcentaje")]
        public int fkIdPorcentaje
        {
            get => porcentajeModelo.pkIdPorcentaje;
            set => porcentajeModelo.pkIdPorcentaje = value;
        }
    }
}

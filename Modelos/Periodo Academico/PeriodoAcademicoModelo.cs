using Colegio.Modelos.Nombre_Periodo_Academico;
using Colegio.Modelos.Porcentaje;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Periodo_Academico
{
    [Table("periodo_academico")]
    public class PeriodoAcademicoModelo
    {

        public NombrePeriodoAcademicoModelo nombrePeriodoAcademicoModelo { get; set; } = new NombrePeriodoAcademicoModelo();
        public PorcentajeModelo porcentajeModelo { get; set; } = new PorcentajeModelo();

        [Column("pk_id_periodo_academico")]
        public int pkIdPeriodoAcademico { get; set; } = 0;

        [Column("fecha_inicio_periodo_academico")]
        public string fechaInicioPeriodoAcademico { get; set; } = string.Empty;

        [Column("fecha_final_periodo_academico")]
        public string fechaFinalPeriodoAcademico { get; set; } = string.Empty;

        [Column("estado_periodo_academico")]
        public string estadoPeriodoAcademico { get; set; } = string.Empty;

        [Column("fk_id_nombre_periodo_academico")]
        public int fkIdNombrePeriodoAcademico
        {
            get => nombrePeriodoAcademicoModelo.pkIdNombrePeriodoAcademico;
            set => nombrePeriodoAcademicoModelo.pkIdNombrePeriodoAcademico = value;
        }

        [Column("fk_id_porcentaje")]
        public int fkIdPorcentaje
        {
            get => porcentajeModelo.pkIdPorcentaje;
            set => porcentajeModelo.pkIdPorcentaje = value;
        }
    }
}

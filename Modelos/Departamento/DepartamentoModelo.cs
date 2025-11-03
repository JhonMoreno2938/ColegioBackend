using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Departamento
{
    [Table("departamento")]
    public class DepartamentoModelo
    {
        [Key]
        [Column("pk_id_departamento")]
        public int pkIdDepartamento { get; set; } = 0;

        [Column("nombre_departamento")]
        public string nombreDepartamento { get; set; } = string.Empty;

        [Column("estado_departamento")]
        public string estadoDepartamento { get; set; } = string.Empty;
        public DepartamentoModelo()
        {

        }

    }
}

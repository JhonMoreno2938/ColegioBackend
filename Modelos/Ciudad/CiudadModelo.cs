using Colegio.Modelos.Departamento;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Colegio.Modelos.Ciudad
{
    [Table("ciudad")]
    public class CiudadModelo
    {
        public DepartamentoModelo departamentoModelo { get; set; } = new DepartamentoModelo();

        [Key]
        [Column("pk_id_ciudad")]
        public int pkIdCiudad { get; set; } = 0;

        [Column("nombre_ciudad")]
        public string nombreCiudad { get; set; } = string.Empty;

        [Column("estado_ciudad")]
        public string estadoCiudad { get; set; } = string.Empty;

        [Column("fk_id_departamento")]
        public int fkIdDepartamento
        {
            get => departamentoModelo.pkIdDepartamento;
            set => departamentoModelo.pkIdDepartamento = value;
        }
        public CiudadModelo()
        {

        }

    }
}

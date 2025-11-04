using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Procedimientos
{
    public class RegistrarGradoGrupoNivelEscolaridad
    {
        [JsonIgnore]
        GradoGrupoModelo gradoGrupoModelo = new GradoGrupoModelo();

        [JsonPropertyName("nombreGrado")]
        [StringLength(2)]
        [Required]
        public string nombreGrado
        {
            get => gradoGrupoModelo.gradoModelo.nombreGrado;
            set => gradoGrupoModelo.gradoModelo.nombreGrado = value;
        }

        [JsonPropertyName("nombreGrupo")]
        [StringLength(3)]
        [Required]
        public string nombreGrupo
        {
            get => gradoGrupoModelo.grupoModelo.nombreGrupo;
            set => gradoGrupoModelo.grupoModelo.nombreGrupo = value;
        }

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNivelEscolaridad
        {
            get => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad;
            set => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad = value;
        }

    }
}

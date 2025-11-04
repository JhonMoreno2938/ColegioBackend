using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grado_Grupo.Procedimientos
{
    public class GestionarEstadoGradoGrupoNivelEscolaridad
    {
        [JsonIgnore]
        private GradoGrupoModelo gradoGrupoModelo = new GradoGrupoModelo();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("nombreGradoGrupo")]
        [StringLength(6)]
        [Required]
        public string nombreGradoGrupo
        {
            get => $"{nombreGrado}-{nombreGrupo}";
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var partes = value.Split('-');
                    nombreGrado = partes[0].Trim();
                    nombreGrupo = partes.Length > 1 ? partes[1].Trim() : string.Empty;
                }
            }
        }

        [JsonIgnore]
        public string nombreGrado
        {
            get => gradoGrupoModelo.gradoModelo.nombreGrado;
            set => gradoGrupoModelo.gradoModelo.nombreGrado = value; 
        }

        [JsonIgnore]
        public string nombreGrupo
        {
            get => gradoGrupoModelo.grupoModelo.nombreGrupo;
            set => gradoGrupoModelo.grupoModelo.nombreGrupo = value;
        }

        [JsonPropertyName("nombreNivelEscolaridad")]
        [StringLength(20)]
        [Required]
        public string nombreNinvelEscolaridad
        {
            get => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad;
            set => gradoGrupoModelo.nivelEscolaridadModelo.nombreNivelEscolaridad = value;
        }
    }
}

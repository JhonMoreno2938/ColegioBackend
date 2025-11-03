using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Procedimientos
{
    public class GestionarEstadoGrupo
    {
        [JsonIgnore]
        GrupoModelo grupoModelo = new GrupoModelo();

        [JsonPropertyName("nombreOperacion")]
        [StringLength(20)]
        [Required]
        public string nombreOperacion { get; set; }

        [JsonPropertyName("nombreGrupo")]
        [StringLength(3)]
        [Required]
        public string nombreGrupo
        {
            get => grupoModelo.nombreGrupo;
            set => grupoModelo.nombreGrupo = value;
        }
    }
}

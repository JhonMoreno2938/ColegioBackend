using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Procedimientos
{
    public class RegistrarGrupo
    {
        [JsonIgnore]
        GrupoModelo grupoModelo = new GrupoModelo();

        [JsonPropertyName("nombre_grupo")]
        [StringLength(3)]
        public string nombreGrupo
        {
            get => grupoModelo.nombreGrupo;
            set => grupoModelo.nombreGrupo = value;
        }
    }
}

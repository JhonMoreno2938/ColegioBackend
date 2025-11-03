using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Vistas
{
    public class ListarGrupoEstadoActivo
    {
        [JsonIgnore]
        GrupoModelo grupoModelo = new GrupoModelo();

        [JsonPropertyName("nombre_grupo")]
        public string nombreGrupo
        {
            get => grupoModelo.nombreGrupo;
            set => grupoModelo.nombreGrupo = value;
        }
    }
}

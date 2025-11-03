using System.Text.Json.Serialization;

namespace Colegio.Modelos.Grupo.Vistas
{
    public class ListarGrupo
    {
        [JsonIgnore]
        GrupoModelo grupoModelo = new GrupoModelo();

        [JsonPropertyName("nombre_grupo")]
        public string nombreGrupo
        {
            get => grupoModelo.nombreGrupo;
            set => grupoModelo.nombreGrupo = value;
        }

        [JsonPropertyName("estado_grupo")]
        public string estadoGrupo
        {
            get => grupoModelo.estadoGrupo;
            set => grupoModelo.estadoGrupo = value;
        }
    }
}
